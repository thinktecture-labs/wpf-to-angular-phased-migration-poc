using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples.UpdateComponentSample;

public static class UpdateComponentSampleModule
{
    public static IServiceCollection AddUpdateComponentSampleEndpoint(this IServiceCollection services) =>
        services.AddSingleton<UpdateComponentSampleValidator>()
                .AddScoped<IUpdateComponentSampleUnitOfWork, InMemoryUpdateComponentSampleUnitOfWork>();
}