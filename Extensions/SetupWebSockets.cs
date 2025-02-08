namespace ExampleWebApi;

public static partial class Extensions
{

    public static void SetupWebSockets(this WebApplication app, CancellationToken cancellationToken)
    {
        app.UseWebSockets(new WebSocketOptions
        {
            KeepAliveInterval = WS_KEEPALIVE_INTERVAL
        });

        app.Use(async (context, next) =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                if (context.Request.Path.ToString().StartsWith(API_WS_PREFIX))
                {
                    var util = context.RequestServices.GetRequiredService<IUtilService>();

                    var wsClientManager = context.RequestServices.GetRequiredService<IWebSocketService>();
                    await wsClientManager.HandleAsync(context, cancellationToken);
                }
                else
                {
                    app.Logger.LogWarning($"invalid request path [{context.Request.Path}]");

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                await next(context);
            }
        });
    }

}