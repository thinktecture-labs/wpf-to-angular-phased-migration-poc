using System;
using Light.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ContactsList;
using WpfApp.DeleteContactDialog;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.CompositionRoot;

public static class DependencyInjection
{
    public static ServiceProvider CreateServiceProvider(IConfiguration configuration) =>
        new ServiceCollection().AddCoreServices(configuration)
                               .AddMainWindow()
                               .AddContactList()
                               .AddDeleteContactDialog()
                               .BuildServiceProvider();

    private static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        var backendUrl = DetermineBackendUrl(configuration);
        return services.AddSingleton(Log.Logger)
                       .AddSingleton<ChronometerFactory>()
                       .AddHttpClient(HttpConstants.BackendHttpClientName,
                                      httpClient => httpClient.BaseAddress = new (backendUrl, UriKind.Absolute))
                       .Services;
    }

    private static IServiceCollection AddMainWindow(this IServiceCollection services) =>
        services.AddSingleton<MainWindow>()
                .AddSingleton<Func<MainWindow>>(sp => sp.GetRequiredService<MainWindow>)
                .AddSingleton<MainWindowViewModel>()
                .AddTransient<INavigator>(sp => sp.GetRequiredService<MainWindowViewModel>());

    private static string DetermineBackendUrl(IConfiguration configuration)
    {
        var backendUrl = configuration["backendUrl"];
        if (backendUrl.IsNullOrWhiteSpace())
            backendUrl = "http://localhost:5000";
        return backendUrl;
    }
}