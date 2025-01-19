namespace ExampleWebApi;

public interface IUtilService
{

    JsonSerializerOptions ConfigureJsonSerializerOptions(JsonSerializerOptions options);

    JsonSerializerOptions JavaSerializerSettings();    

}