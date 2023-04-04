using Backend.ComponentSamples;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Backend.CompositionRoot;

public static class HttpPipeline
{
    public static WebApplication ConfigureHttpPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseCorsIfNecessary();
        app.UseSpaStaticFiles();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.MapHealthChecks("/health");
        app.MapComponentSampleEndpoints();
        app.UseSpa(spa => spa.Options.SourcePath = "./angular-app");

        return app;
    }
}