using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ContactsList;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.CompositionRoot;

public static class DependencyInjection
{
    public static ServiceProvider CreateServiceProvider() =>
        new ServiceCollection()
           .AddSingleton(Log.Logger)
           .AddSingleton<ChronometerFactory>()
           .AddSingleton<MainWindow>()
           .AddSingleton<MainWindowViewModel>()
           .AddContactList()
           .AddTransient<INavigator>(sp => sp.GetRequiredService<MainWindowViewModel>())
           .AddHttpClient(HttpConstants.BackendHttpClientName,
                httpClient => httpClient.BaseAddress = new ("http://localhost:5000", UriKind.Absolute))
           .Services
           .BuildServiceProvider();
}