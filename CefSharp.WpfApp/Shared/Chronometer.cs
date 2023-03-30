using System;
using System.Diagnostics;
using Serilog;

namespace CefSharp.WpfApp.Shared;

public readonly struct Chronometer : IDisposable
{
    public Chronometer(string topic, Stopwatch stopwatch, ILogger logger)
    {
        Topic = topic;
        Stopwatch = stopwatch;
        Logger = logger;
    }

    private static Stopwatch StopwatchSingleton { get; } = new ();

    private string Topic { get; }
    private Stopwatch Stopwatch { get; }
    private ILogger Logger { get; }

    public void Dispose()
    {
        Stopwatch.Stop();
        Logger.Information("{Topic} took {Milliseconds}ms", Topic, Stopwatch.ElapsedMilliseconds);
    }
}