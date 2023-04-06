using System;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public sealed class ComponentSampleDto
{
    // ReSharper disable InconsistentNaming -- we need lowerCamelCase naming for CefSharp DTO exchange
    // ReSharper disable MemberCanBePrivate.Global -- must be public for serialization
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable PropertyCanBeMadeInitOnly.Global
    public string id { get; set; } = string.Empty;
    public string componentName { get; set; } = string.Empty;
    public string migrationTime { get; set; } = string.Empty;
    public double peakArea { get; set; }
    // ReSharper restore PropertyCanBeMadeInitOnly.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore MemberCanBePrivate.Global
    // ReSharper restore InconsistentNaming

    public static ComponentSampleDto FromComponentSample(ComponentSample componentSample) =>
        new ()
        {
            id = componentSample.Id.ToString(),
            componentName = componentSample.ComponentName,
            migrationTime = componentSample.MigrationTime.ToString(@"hh\:mm\:ss"),
            peakArea = Convert.ToDouble(componentSample.PeakArea)
        };
}