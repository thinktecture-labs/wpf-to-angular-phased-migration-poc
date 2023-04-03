using System;

namespace Backend.DataAccess;

public sealed class ComponentSample : IComponentSampleProperties
{
    public Guid Id { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public TimeSpan MigrationTime { get; set; }
    public decimal PeakArea { get; set; }

    public void CopyValuesTo(ComponentSample other)
    {
        other.ComponentName = ComponentName;
        other.MigrationTime = other.MigrationTime;
        other.PeakArea = PeakArea;
    }
}