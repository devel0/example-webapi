
namespace ExampleWebApi;

public class SampleService : ISampleService
{
    readonly ILogger logger;
    readonly IHostEnvironment environment;
    readonly IConfiguration configuration;

    public SampleService(
        ILogger<SampleService> logger,
        IHostEnvironment environment,
        IConfiguration configuration
    )
    {
        this.logger = logger;
        this.environment = environment;
        this.configuration = configuration;
    }

    public async Task<int> Sum(int x, int y, CancellationToken cancellationToken)
    {
        logger.LogDebug($"doing some work with sample service");

        var appConfig = configuration.GetSection(AppSettings_AppConfig).Get<AppConfig>();
        if (appConfig?.SampleObject?.SampleVar is not null)
            logger.LogDebug($"{nameof(AppConfig)} -> {nameof(AppConfig.SampleObject)} -> {nameof(AppConfig.SampleObject.SampleVar)} : {appConfig.SampleObject.SampleVar}");         

        return x + y;
    }    

}