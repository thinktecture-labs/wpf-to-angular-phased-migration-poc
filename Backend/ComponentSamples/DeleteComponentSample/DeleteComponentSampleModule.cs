using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples.DeleteComponentSample;

public static class DeleteComponentSampleModule
{
    public static IServiceCollection AddDeleteComponentSampleEndpoint(this IServiceCollection services) =>
        services.AddScoped<IDeleteComponentSampleUnitOfWork, InMemoryDeleteComponentSampleUnitOfWork>();
}