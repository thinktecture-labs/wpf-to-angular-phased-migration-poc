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

        using (ServiceProvider.GetRequiredService<ChronometerFactory>().Create("Chromium Embedded Framework initialization"))
        {
            // It's important that Initialize/Shutdown MUST be called on your main application thread (typically the UI thread).
            // If you call them on different threads, your application will hang.
            ChromiumEmbedded.Initialize();
        }

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        MainWindow.Show();
        mainWindow.ChromiumWebBrowser.Load("https://duckduckgo.com/");
    }

    protected override void OnExit(ExitEventArgs e) => ServiceProvider.Dispose();
}