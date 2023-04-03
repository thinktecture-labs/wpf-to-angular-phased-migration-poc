using System;

namespace Backend.DataAccess;

public interface IComponentSampleProperties
{
    string ComponentName { get; set; }
    TimeSpan MigrationTime { get; set; }
    decimal PeakArea { get; set; }
}