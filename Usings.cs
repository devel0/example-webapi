// system
global using static System.Net.Mime.MediaTypeNames;
global using System.Diagnostics;
global using System.IO.Compression;
global using System.Reflection;

// microsoft
global using ILogger = Microsoft.Extensions.Logging.ILogger;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.ResponseCompression;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.OpenApi.Models;

// thirdy
global using Serilog;
global using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

// this
global using static ExampleWebApi.Constants;
global using static ExampleWebApi.Toolkit;
global using ExampleWebApi;