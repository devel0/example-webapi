namespace ExampleWebApi;

public partial class WebSocketService : IWebSocketService
{

    readonly ILogger logger;
    readonly IUtilService util;

    public WebSocketService(
        IUtilService pubUtil,
        ILogger<WebSocketService> logger
    )
    {
        this.util = pubUtil;
        this.logger = logger;
    }

    static ConcurrentDictionary<WebSocketNfo, bool> connections = new ConcurrentDictionary<WebSocketNfo, bool>();

    async Task Send<T>(WebSocketNfo wsNfo, T obj, CancellationToken cancellationToken) where T : WSProtocol
    {
        await wsNfo.SendSem.WaitAsync(cancellationToken);

        try
        {
            await util.SendMessageAsync(obj, wsNfo.webSocket, cancellationToken);
        }
        finally
        {
            wsNfo.SendSem.Release();
        }
    }

    public async Task SendToAllClientsAsync<T>(
       T obj,
       CancellationToken cancellationToken) where T : WSProtocol
    {
        logger.LogTrace($"dispatching to {connections.Count} clients");

        var clients = connections.ToList().Select(w => w.Key).ToList();

        foreach (var client in clients)
        {
            await Send(client, obj, cancellationToken);
        }
    }

    public async Task HandleAsync(HttpContext httpContext, CancellationToken cancellationToken)
    {
        var ctx = new WebSocketAcceptContext
        {
            KeepAliveInterval = TimeSpan.FromSeconds(30)
        };
        using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync(ctx);

        //---
        // Use of authentication service if available ( see https://github.com/devel0/example-webapp-with-auth/blob/main/src/backend/webapi/Abstractions/IAuthService.cs#L28 )
        //---

        // var curUserRes = await auth.CurrentUserNfoAsync(cancellationToken);

        // if (curUserRes is null)
        // {
        //     logger.LogError($"can't find valid user on ws conn");
        //     return;
        // }

        var wsCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        var wsNfo = new WebSocketNfo(webSocket);

        await Task.Factory.StartNew(async () =>
        {
            while (!wsCts.Token.IsCancellationRequested)
            {
                // use of wsCts.Token cancellation token to honor socket close
                await Send(wsNfo, new WSSomeEvent($"test msg {DateTime.UtcNow}"), wsCts.Token);

                try
                {
                    await Task.Delay(1000, wsCts.Token);
                }
                catch (OperationCanceledException)
                {
                    logger.LogDebug($"websocket closed then stop associated task");
                    break;
                }
            }
        }, wsCts.Token);

        connections.TryAdd(wsNfo, true);

        logger.LogTrace($"Websocket connected");

        while (!cancellationToken.IsCancellationRequested)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var rxObjNfo = await util.ReceiveMessageAsync<WSProtocol>(webSocket, cancellationToken);

                if (rxObjNfo.obj is not null && rxObjNfo.str is not null)
                {

                    switch (rxObjNfo.obj.MessageType)
                    {

                        case WSMessageType.Ping:
                            {
                                var ping = JsonSerializer.Deserialize<WSPing>(rxObjNfo.str, util.JavaSerializerSettings());
                                if (ping is not null)
                                {
                                    await util.SendMessageAsync(new WSPong(ping.Msg), webSocket, cancellationToken);
                                }
                            }
                            break;

                    }

                }

            }
            else break;
        }

        try
        {
            wsCts.Cancel();

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                statusDescription: "", cancellationToken: cancellationToken);
        }
        catch (WebSocketException wex)
        {
            logger.LogWarning(wex, "websocket exception");
        }

        logger.LogTrace($"Web socket closed");

        connections.TryRemove(wsNfo, out var _);
    }

}