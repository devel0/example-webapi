namespace ExampleWebApi.Controllers;

[ApiController]
[Route($"{API_PREFIX}/[controller]/[action]")]
public class SampleController : ControllerBase
{
    readonly ILogger logger;
    readonly ISampleService sample;
    readonly CancellationToken cancellationToken;
    readonly IHttpContextAccessor httpContextAccessor;

    public SampleController(
        ILogger<SampleController> logger,
        ISampleService sample,
        CancellationToken cancellationToken,
        IHttpContextAccessor httpContextAccessor
    )
    {
        this.logger = logger;
        this.sample = sample;
        this.cancellationToken = cancellationToken;
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Documentation here will available in swagger and in generated api through openapi
    /// </summary>
    /// <param name="x">1th member</param>
    /// <param name="y">2th member</param>
    /// <returns>sum of members</returns>
    [HttpGet]
    public async Task<int> Sum(int x, int y) => await sample.Sum(x, y, cancellationToken);

    [HttpGet]
    public async Task<IActionResult> SampleWsData()
    {
        var context = httpContextAccessor.HttpContext;

        if (context is not null)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var util = context.RequestServices.GetRequiredService<IUtilService>();

                var wsClientManager = context.RequestServices.GetRequiredService<IWebSocketService>();
                await wsClientManager.HandleAsync(context, cancellationToken);
            }

            else
                return StatusCode((int)HttpStatusCode.UpgradeRequired);
        }

        return Ok();
    }

}
