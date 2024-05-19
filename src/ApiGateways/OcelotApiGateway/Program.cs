using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

#pragma warning disable ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile(
        $"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json",
        true, 
        true
        );
});
#pragma warning restore ASP0013 // Suggest switching from using Configure methods to WebApplicationBuilder.Configuration

builder.Services.AddLogging();
var loggingConfig = builder.Configuration.GetSection("Logging");

builder.Logging
    .AddConfiguration(loggingConfig)
    .AddConsole()
    .AddDebug();

builder.Services
    .AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.UseRouting();
await app.UseOcelot();

app.Run();
