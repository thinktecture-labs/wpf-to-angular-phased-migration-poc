using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples.CreateComponentSample;

public static class CreateComponentSampleModule
{
    public static IServiceCollection AddCreateComponentSampleEndpoint(this IServiceCollection services) =>
        services.AddSingleton<CreateComponentSampleDtoValidator>()
                .AddScoped<ICreateComponentSampleUnitOfWork, InMemoryCreateComponentSampleUnitOfWork>();
}