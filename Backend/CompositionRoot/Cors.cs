using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.CompositionRoot;

public static class Cors
{
    public static IServiceCollection AddCorsIfNecessary(this IServiceCollection services,
                                                        IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment())
            services.AddCors();

        return services;
    }

    public static WebApplication UseCorsIfNecessary(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        var allowedCorsOrigins = app.Configuration.GetSection("allowedCorsOrigins").Get<string[]>();
        if (allowedCorsOrigins is not null && allowedCorsOrigins.Length > 0)
        {
            app.UseCors(builder => builder.WithOrigins(allowedCorsOrigins)
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
        }

        return app;
    }
}