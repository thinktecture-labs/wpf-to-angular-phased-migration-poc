using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Backend.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLogger() =>
        new LoggerConfiguration().WriteTo.Console()
                                 .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                 .CreateBootstrapLogger();

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
        builder.Services.AddSingleton(Log.Logger);
        return builder;
    }
}