using System.Windows;
using CefSharp.WpfApp.CompositionRoot;
using CefSharp.WpfApp.Shared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CefSharp.WpfApp;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class App : Application
{
    public App()
    {
        Log.Logger = Logging.CreateLogger();
        ServiceProvider =
            new ServiceCollection()
               .AddSingleton(Log.Logger)
               .AddSingleton<ChronometerFactory>()
               .AddSingleton<MainWindow>()
               .BuildServiceProvider();
    }
    
    private ServiceProvider ServiceProvider { get; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e) => ServiceProvider.Dispose();
}