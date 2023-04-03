using System;
using Backend.DataAccess;

namespace Backend.ComponentSamples.CreateComponentSample;

public sealed class CreateComponentSampleDto : IComponentSampleProperties
{
    public string ComponentName { get; set; } = string.Empty;
    public TimeSpan MigrationTime { get; set; }
    public decimal PeakArea { get; set; }

    public ComponentSample ToComponentSample() => new ()
    {
        Id = Guid.NewGuid(),
        ComponentName = ComponentName,
        MigrationTime = MigrationTime,
        PeakArea = PeakArea
    };
}