using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples.GetComponentSamples;

public static class GetComponentSamplesModule
{
    public static IServiceCollection AddGetComponentSamplesEndpoint(this IServiceCollection services) =>
        services.AddScoped<IGetComponentSamplesUnitOfWork, InMemoryGetComponentSamplesUnitOfWork>();
}