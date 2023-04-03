using System;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.ComponentSampleList;

public static class ComponentSampleListModule 
{
    public static IServiceCollection AddComponentSampleList(this IServiceCollection services) =>
        services.AddTransient<ComponentSampleListViewModel>()
                .AddTransient<Func<ComponentSampleListViewModel>>(sp => sp.GetRequiredService<ComponentSampleListViewModel>)
                .AddTransient<IComponentSamplesSession, HttpComponentSamplesSession>()
                .AddSingleton<Func<IComponentSamplesSession>>(sp => sp.GetRequiredService<IComponentSamplesSession>)
                .AddSingleton<INavigateToComponentSampleListCommand, NavigateToComponentSampleListCommand>();
}