﻿using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ComponentSampleList;
using WpfApp.CompositionRoot;

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

        try
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;
            MainWindow.Show();
        
            ServiceProvider.GetRequiredService<INavigateToComponentSampleListCommand>()
                           .Navigate();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Could not start WPF app");
            Shutdown(1);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        ServiceProvider.Dispose();
    }
}