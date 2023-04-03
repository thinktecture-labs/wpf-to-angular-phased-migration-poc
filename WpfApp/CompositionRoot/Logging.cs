using System.Diagnostics;
using Serilog;

namespace WpfApp.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLogger()
    {
        var configuration = new LoggerConfiguration().WriteTo.File("./WpfApp.log");
        if (Debugger.IsAttached)
            configuration.WriteTo.Debug();

        return configuration.CreateLogger();
    }
}