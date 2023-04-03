﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.CompositionRoot;
using WpfApp.ContactsList;

namespace WpfApp;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class App : Application
{
    public App()
    {
        Log.Logger = Logging.CreateLogger();
        var configuration = Configuration.Create();
        ServiceProvider = DependencyInjection.CreateServiceProvider(configuration);
    }
    
    private ServiceProvider ServiceProvider { get; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        MainWindow.Show();
        
        ServiceProvider.GetRequiredService<INavigateToContactsListCommand>()
                       .Navigate();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        ServiceProvider.Dispose();
    }
}