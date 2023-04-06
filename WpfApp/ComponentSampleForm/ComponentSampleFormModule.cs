using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ComponentSampleList;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public static class ComponentSampleFormModule
{
    public static IServiceCollection AddComponentSampleForm(this IServiceCollection services) =>
        services.AddTransient<ComponentSampleFormViewModel>()
                .AddSingleton<Func<ComponentSample, List<ComponentSample>, ComponentSampleFormViewModel>>(sp =>
                 {
                     return (sample, allSamples) => new (sample,
                                                         allSamples,
                                                         sp.GetRequiredService<INotificationPublisher>(),
                                                         sp.GetRequiredService<INavigateToComponentSampleListCommand>(),
                                                         sp.GetRequiredService<Func<ISampleFormSession>>(),
                                                         sp.GetRequiredService<ILogger>());
                 })
                .AddTransient<ISampleFormSession, HttpSampleFormSession>()
                .AddSingleton<Func<ISampleFormSession>>(sp => sp.GetRequiredService<ISampleFormSession>)
                .AddSingleton<INavigateToFormCommand, NavigateToFormCommand>();
}