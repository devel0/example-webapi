namespace ExampleWebApi;

public static partial class Extensions
{

    public static void SetupWebSockets(this WebApplication app, CancellationToken cancellationToken)
    {
        app.UseWebSockets(new WebSocketOptions
        {
            KeepAliveInterval = WS_KEEPALIVE_INTERVAL
        });        
    }

}