using System;

namespace WpfApp.Shared;

public sealed class ComponentSample
{
    public Guid Id { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public TimeSpan MigrationTime { get; set; }
    public decimal PeakArea { get; set; }
}