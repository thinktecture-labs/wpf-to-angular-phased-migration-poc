using Backend.ComponentSamples.CreateComponentSample;
using Backend.ComponentSamples.DeleteComponentSample;
using Backend.ComponentSamples.GetComponentSample;
using Backend.ComponentSamples.GetComponentSamples;
using Backend.ComponentSamples.UpdateComponentSample;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.ComponentSamples;

public static class ComponentSamplesModule
{
    public static IServiceCollection AddComponentSampleEndpoints(this IServiceCollection services) =>
        services.AddGetComponentSamplesEndpoint()
                .AddGetComponentSampleEndpoint()
                .AddCreateComponentSampleEndpoint()
                .AddUpdateComponentSampleEndpoint()
                .AddDeleteComponentSampleEndpoint();

    public static WebApplication MapComponentSampleEndpoints(this WebApplication app) =>
        app.MapGetComponentSamples()
           .MapGetComponentSample()
           .MapCreateComponentSample()
           .MapUpdateComponentSample()
           .MapDeleteComponentSample();
}