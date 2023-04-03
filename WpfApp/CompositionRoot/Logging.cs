using System.Diagnostics;
using Serilog;

namespace WpfApp.CompositionRoot;

public static class Logging
{
    public static ILogger CreateLogger()
    {
        var configuration =
            new LoggerConfiguration()
               .WriteTo.File("./WpfApp.log",
                             rollOnFileSizeLimit: true,
                             fileSizeLimitBytes: 1024 * 1024 * 1024,
                             retainedFileCountLimit: 5);
        
        if (Debugger.IsAttached)
            configuration.WriteTo.Debug();

        return configuration.CreateLogger();
    }
}