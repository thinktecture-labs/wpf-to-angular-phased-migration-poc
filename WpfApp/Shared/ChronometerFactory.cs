using System.Diagnostics;
using Serilog;

namespace WpfApp.Shared;

public sealed class ChronometerFactory
{
    public ChronometerFactory(ILogger logger)
    {
        Logger = logger;
    }

    private ILogger Logger { get; }

    public Chronometer Create(string topic) => new (topic, Stopwatch.StartNew(), Logger);
}