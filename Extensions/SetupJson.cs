namespace ExampleWebApi;

public static partial class Extensions
{
     
    public static void SetupJson(this IServiceCollection services)
    {
        // https://stackoverflow.com/a/76644093

        services.ConfigureHttpJsonOptions(options =>
        {
            var sp = services.BuildServiceProvider();

            var util = sp.GetRequiredService<IUtilService>();

            util.ConfigureJsonSerializerOptions(options.SerializerOptions);
        });

        services.Configure<JsonOptions>(options =>
        {
            var sp = services.BuildServiceProvider();

            var util = sp.GetRequiredService<IUtilService>();

            util.ConfigureJsonSerializerOptions(options.JsonSerializerOptions);

            // options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());            
        });

    }

}
