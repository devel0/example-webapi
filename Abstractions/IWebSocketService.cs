namespace ExampleWebApi;

public class WebSocketNfo
{
    //, CurrentUserResponseDto user); // add auth user nfo if available
    public WebSocketNfo(WebSocket webSocket)
    {
        this.webSocket = webSocket;        
    }

    public WebSocket webSocket { get; }    

    /// <summary>
    /// semaphore for websocket send
    /// </summary>
    public SemaphoreSlim SendSem { get; } = new SemaphoreSlim(1);

}

/// <summary>
/// Handle ingress websocket connections
/// </summary>
public interface IWebSocketService
{

    /// <summary>
    /// Manage websocket connection and spawn a ws handler.
    /// </summary>
    Task HandleAsync(HttpContext httpContext, CancellationToken cancellationToken);

    /// <summary>
    /// send generic event notify to all clients
    /// </summary>
    Task SendToAllClientsAsync<T>(T obj, CancellationToken cancellationToken) where T : WSProtocol;

}