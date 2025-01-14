namespace ExampleWebApi;

public static partial class Extensions
{

    /// <summary>
    /// Connects host application lifetime started, stopping, stopped magaging cancellation of given token source
    /// </summary>
    public static IHostApplicationLifetime SetupLifetime(this WebApplication app, CancellationTokenSource cts)
    {
        var life = app.Services.GetRequiredService<IHostApplicationLifetime>();
        var configuration = app.Services.GetRequiredService<IConfiguration>();

        life.ApplicationStarted.Register(() =>
        {
            app.Logger.LogInformation("Backend application started");
        });

        life.ApplicationStopping.Register(async () =>
        {
            app.Logger.LogInformation("Backend application stopping");                    
        });

        life.ApplicationStopped.Register(() =>
        {
            app.Logger.LogInformation("Backend application stopped");
        });

        return life;

    }

}