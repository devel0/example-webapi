namespace ExampleWebApi;

public class UtilService : IUtilService
{

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

}