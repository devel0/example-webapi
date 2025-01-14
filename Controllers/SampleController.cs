namespace ExampleWebApi.Controllers;

[ApiController]
[Route($"{API_PREFIX}/[controller]/[action]")]
public class SampleController : ControllerBase
{
    readonly ILogger logger;
    readonly ISampleService sample;
    readonly CancellationToken cancellationToken;

    public SampleController(
        ILogger<SampleController> logger,
        ISampleService sample,
        CancellationToken cancellationToken
    )
    {
        this.sample = sample;
        this.cancellationToken = cancellationToken;
    }

    /// <summary>
    /// Documentation here will available in swagger and in generated api through openapi
    /// </summary>
    /// <param name="x">1th member</param>
    /// <param name="y">2th member</param>
    /// <returns>sum of members</returns>
    [HttpGet]
    public async Task<int> Sum(int x, int y) => await sample.Sum(x, y, cancellationToken);

}
