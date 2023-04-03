using Backend.ComponentSamples;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Backend.CompositionRoot;

public static class HttpPipeline
{
    public static WebApplication ConfigureHttpPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapHealthChecks("/");
        app.MapComponentSampleEndpoints();

        return app;
    }
}