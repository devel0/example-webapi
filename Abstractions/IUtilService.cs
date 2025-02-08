namespace ExampleWebApi;

public interface IUtilService
{

    JsonSerializerOptions ConfigureJsonSerializerOptions(JsonSerializerOptions options);

    JsonSerializerOptions JavaSerializerSettings();

    /// <summary>
    /// Retrieve <see cref="AppConfig"/> from appsettings.
    /// </summary>
    AppConfig? Config();

    /// <summary>
    /// Retrieve required <see cref="AppConfig"/> from appsettings.
    /// </summary>
    AppConfig RequiredConfig();

    /// <summary>
    /// Receive message from websocket using default java serializer and service logger;
    /// returns string original response to allow further specialized deserialization.
    /// </summary>            
    Task<(T? obj, string? str)> ReceiveMessageAsync<T>(WebSocket webSocket, CancellationToken cancellationToken);

    /// <summary>
    /// Send object to given websocket through serialization.
    /// </summary>
    Task<bool> SendMessageAsync<T>(T msg, WebSocket webSocket, CancellationToken cancellationToken);

}