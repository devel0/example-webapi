namespace ExampleWebApi;

public interface IGracefulShutdownService
{

    Task HandleGracefulShutdown();

}