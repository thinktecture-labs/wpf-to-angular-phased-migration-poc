using System.Diagnostics;
using Serilog;

namespace CefSharp.WpfApp.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLogger()
    {
        var configuration = new LoggerConfiguration().WriteTo.File("./CefSharp.WpfApp.log");
        if (Debugger.IsAttached)
            configuration.WriteTo.Debug();

        return configuration.CreateLogger();
    }
}