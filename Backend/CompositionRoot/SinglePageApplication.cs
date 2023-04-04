using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.CompositionRoot;

public static class SinglePageApplication
{
    private static string AngularAppPath { get; } =
        Path.Combine(
            Path.GetDirectoryName(typeof(SinglePageApplication).Assembly.Location)!,
            "angular-app"
        );

    public static IServiceCollection AddSinglePageApplication(this IServiceCollection services)
    {
        services.AddSpaStaticFiles(options => options.RootPath = AngularAppPath);
        return services;
    }

    public static WebApplication UseSinglePageApplication(this WebApplication app)
    {
        app.UseSpa(builder => builder.Options.SourcePath = AngularAppPath);
        return app;
    }
}