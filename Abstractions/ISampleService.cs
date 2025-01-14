namespace ExampleWebApi;

public interface ISampleService
{

    Task<int> Sum(int x, int y, CancellationToken cancellationToken);    

}