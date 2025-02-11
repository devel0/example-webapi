var cts = new CancellationTokenSource();

var builder = WebApplication.CreateBuilder(args);

// load config from appsettings, environment and user-secrets
builder.Configuration.SetupAppSettings(builder.Environment.EnvironmentName);

// add basic services
builder.Services.AddScoped(typeof(CancellationToken), sp => cts.Token);
builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });
builder.Services.AddSerilog(config => { config.ReadFrom.Configuration(builder.Configuration); });
builder.Services.AddHttpContextAccessor();

// add custom services
builder.Services.AddScoped<IGracefulShutdownService, GracefulShutdownService>();
builder.Services.AddScoped<ISampleService, SampleService>();
builder.Services.AddScoped<IUtilService, UtilService>();
builder.Services.AddScoped<IWebSocketService, WebSocketService>();

// add controllers
builder.Services.AddControllers();

// add openapi
builder.Services.AddOpenApi();

// add swagger
builder.Services.AddSwaggerGenCustom();

//----------------------------------------------------------------------------------
// APP BUILD
//----------------------------------------------------------------------------------

var app = builder.Build();

// use response compression
app.UseResponseCompression();

// custommize exception handling
app.SetupException();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.ConfigSwagger();

app.UseHttpsRedirection();

// add websocket support
app.SetupWebSockets(cts.Token);

app.UseAuthorization();

app.MapControllers();

//----------------------------------------------------------------------------------
// APP START
//----------------------------------------------------------------------------------

// start app
await app.StartAsync(cts.Token);

app.Logger.LogInformation("Application started");

app.Logger.LogInformation($"Environment: {app.Environment.EnvironmentName}");

app.Logger.LogInformation($"Listening on {string.Join(" ", app.Urls.Select(w => w.ToString()))}");

await app.WaitForShutdownAsync(cts.Token);

var gracefulShutdown = app.Services.GetRequiredService<IGracefulShutdownService>();

// app run
try
{
    await app.RunAsync(cts.Token);
}
catch (OperationCanceledException)
{
    app.Logger.LogInformation("Application stopping");
}

await gracefulShutdown.HandleGracefulShutdown();

app.Logger.LogInformation("Application stopped");