namespace ExampleWebApi;

public static partial class Extensions
{

    /// <summary>
    /// setup application configuration to read configuration from appsettings.json, optional
    /// appsettings.(env).json, environment variables ( : replaced by __ ) and user secrets
    /// </summary>
    public static IConfiguration SetupConfiguration(this IHostApplicationBuilder builder) =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

}