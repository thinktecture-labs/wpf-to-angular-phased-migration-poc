using System;
using CefSharp.WpfApp.ContactsList;
using CefSharp.WpfApp.Http;
using CefSharp.WpfApp.Shared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CefSharp.WpfApp.CompositionRoot;

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