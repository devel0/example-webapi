namespace ExampleWebApi;

public class UtilService : IUtilService
{

    readonly ILogger logger;
    readonly IConfiguration configuration;

    public UtilService(
        ILogger<UtilService> logger,
        IConfiguration configuration
    )
    {
        this.logger = logger;
        this.configuration = configuration;
    }

    public JsonSerializerOptions ConfigureJsonSerializerOptions(JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonStringEnumConverter());
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;

        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = {
                (t) =>
                {
                     // ignore others by property name filter                    

                    // if (t.Type == typeof(ApplicationUser))
                    // {
                    //     foreach (var prop in t.Properties)
                    //     {
                    //         prop.ShouldSerialize = (obj, _) =>
                    //             prop.Name == nameof(ApplicationUser.UserName).ToCamelCase()
                    //             ||
                    //             prop.Name == nameof(ApplicationUser.Id).ToCamelCase()
                    //             ;
                    //     }
                    //     ;
                    // }

                    // ignore by attribute example

                    // foreach (var prop in t.Properties)
                    // {
                    //     var toignore = prop.AttributeProvider?
                    //         .GetCustomAttributes(false)
                    //         .OfType<SomeAttribute>()
                    //         .Count() > 0;

                    //     if (toignore)
                    //     {
                    //         prop.ShouldSerialize = (obj, _) => { return false; };
                    //     }
                    // }
                }
            }
        };

        return options;
    }

    public JsonSerializerOptions JavaSerializerSettings()
    {
        var options = new JsonSerializerOptions();

        ConfigureJsonSerializerOptions(options);

        return options;
    }

    public AppConfig? Config()
    {
        var appConfig = configuration.GetSection(AppSettings_AppConfig).Get<AppConfig>();

        return appConfig;
    }

    public AppConfig RequiredConfig()
    {
        var res = Config();

        if (res is null) throw new Exception($"invalid null AppConfig");

        return res;
    }

    public async Task<(T? obj, string? str)> ReceiveMessageAsync<T>(WebSocket webSocket, CancellationToken cancellationToken)
    {
        T? res = default;
        var str = await webSocket.ReceiveStringAsync(cancellationToken);
        if (!string.IsNullOrWhiteSpace(str))
            res = JsonSerializer.Deserialize<T>(str, JavaSerializerSettings());

        return (res, str);
    }

    public async Task<bool> SendMessageAsync<T>(T msg, WebSocket webSocket, CancellationToken cancellationToken)
    {
        var str = JsonSerializer.Serialize(msg, JavaSerializerSettings());

        var res = await webSocket.SendStringAsync(str, cancellationToken);

        return res;
    }

}