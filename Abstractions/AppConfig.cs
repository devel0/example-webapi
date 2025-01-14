namespace ExampleWebApi;

/// <summary>
/// Type belonging to <see cref="AppSettings_AppConfig"/> ; extends this object as needed to configure
/// application through the appsettings json files.
/// </summary>
public class AppConfig
{
    public SampleObjectCls? SampleObject { get; set; }

    public class SampleObjectCls
    {

        public string? SampleVar { get; set; }

    }

}