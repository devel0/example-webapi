
namespace ExampleWebApi;

public class GracefulShutdownService : IGracefulShutdownService
{
    readonly ILogger logger;
    readonly IServiceScope scope;
    readonly IServiceProvider sp;

    public GracefulShutdownService(
        IServiceProvider sp
    )
    {
        this.scope = sp.CreateScope();
        this.sp = scope.ServiceProvider;
                
        this.logger = this.sp.GetRequiredService<ILogger<GracefulShutdownService>>();        
    }

    public async Task HandleGracefulShutdown()
    {
        logger.LogInformation("Graceful shutdown in progress");
        logger.LogInformation("Fake 3 sec wait");
        await Task.Delay(3000);
        logger.LogInformation("Graceful shutdown completed");
    }

}