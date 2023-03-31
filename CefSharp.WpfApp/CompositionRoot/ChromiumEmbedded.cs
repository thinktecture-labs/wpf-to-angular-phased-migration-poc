using System;
using System.IO;
using CefSharp.Wpf;
using CefSharp.WpfApp.Shared;

namespace CefSharp.WpfApp.CompositionRoot;

public static class ChromiumEmbedded
{
    public static void InitializeAndMeasure(ChronometerFactory chronometerFactory)
    {
        // It's important that Initialize/Shutdown MUST be called on your main application thread
        // (typically the UI thread). If you call them on different threads, your application will hang.
        using (chronometerFactory.Create("Chromium Embedded Framework initialization"))
        {
            Initialize();
        }
    }

    private static void Initialize()
    {
        var settings = new CefSettings();
        // By default CEF uses an in memory cache, to save cached data e.g. to persist cookies you need to
        // specify a cache path. NOTE: The executing user must have sufficient privileges to write to this folder.
        settings.CachePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "WpfNgPoc",
            "ChromiumEmbeddedCache"
        );
        settings.LogFile = Path.Combine(
            Path.GetDirectoryName(typeof(ChromiumEmbedded).Assembly.Location)!,
            "Chromium.log"
        );
        settings.LogSeverity = LogSeverity.Info;
        Cef.Initialize(settings);
    }
}