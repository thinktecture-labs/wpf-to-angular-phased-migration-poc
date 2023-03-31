using System.Windows;
using CefSharp.WpfApp.CompositionRoot;
using CefSharp.WpfApp.ContactsList;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CefSharp.WpfApp;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class App : Application
{
    public App()
    {
        Log.Logger = Logging.CreateLogger();
        ServiceProvider = DependencyInjection.CreateServiceProvider();
    }
    
    private ServiceProvider ServiceProvider { get; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        MainWindow.Show();
        
        ServiceProvider.GetRequiredService<NavigateToContactsListCommand>()
                       .NavigateToContactList();
    }

    protected override void OnExit(ExitEventArgs e) => ServiceProvider.Dispose();
}