using System;
using Light.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ContactsList;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.CompositionRoot;

public static class DependencyInjection
{
    public static ServiceProvider CreateServiceProvider(IConfiguration configuration)
    {
        var backendUrl = DetermineBackendUrl(configuration);
        return new ServiceCollection()
              .AddSingleton(Log.Logger)
              .AddSingleton(configuration)
              .AddSingleton<ChronometerFactory>()
              .AddSingleton<MainWindow>()
              .AddSingleton<MainWindowViewModel>()
              .AddContactList()
              .AddTransient<INavigator>(sp => sp.GetRequiredService<MainWindowViewModel>())
              .AddHttpClient(HttpConstants.BackendHttpClientName,
                             httpClient => httpClient.BaseAddress = new (backendUrl, UriKind.Absolute))
              .Services
              .BuildServiceProvider();
    }

    private static string DetermineBackendUrl(IConfiguration configuration)
    {
        var backendUrl = configuration["backendUrl"];
        if (backendUrl.IsNullOrWhiteSpace())
            backendUrl = "http://localhost:5000";
        return backendUrl;
    }
}