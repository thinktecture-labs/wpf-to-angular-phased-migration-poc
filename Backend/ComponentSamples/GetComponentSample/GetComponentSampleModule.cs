using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples.GetComponentSample;

public static class GetComponentSampleModule
{
    public static IServiceCollection AddGetComponentSampleEndpoint(this IServiceCollection services) =>
        services.AddScoped<IGetComponentSampleUnitOfWork, InMemoryComponentSampleUnitOfWork>(); 
}