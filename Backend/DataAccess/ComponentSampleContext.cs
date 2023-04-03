using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Bogus;
using Serilog;

namespace Backend.DataAccess;

public sealed class ComponentSampleContext
{
    private const string ComponentSamplesFilePath = "component-samples.json";

    public ComponentSampleContext(List<ComponentSample> componentSamples, ILogger logger)
    {
        ComponentSamples = componentSamples;
        ComponentSamplesLookup = componentSamples.ToDictionary(c => c.Id);
        Logger = logger;
    }

    private static JsonSerializerOptions JsonOptions { get; } =
        new () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };

    public List<ComponentSample> ComponentSamples { get; }
    public Dictionary<Guid, ComponentSample> ComponentSamplesLookup { get; }
    private ILogger Logger { get; }
    public bool IsDirty { get; private set; }

    public void SaveChanges() => IsDirty = true;

    public static ComponentSampleContext CreateDefault(ILogger logger,
                                                       Faker<ComponentSample> sampleFaker)
    {
        var componentSamples = GetComponentSamplesFromFile(logger);
        if (componentSamples == null)
        {
            var componentNames = ComponentNames.All;
            componentSamples = new (componentNames.Length);
            for (var i = 0; i < componentNames.Length; i++)
            {
                var componentName = componentNames[i];
                var sample = sampleFaker.Generate();
                sample.ComponentName = componentName;
                componentSamples.Add(sample);
            }
            
            logger.Information("Component samples generated");
        }

        return new (componentSamples, logger);
    }

    private static List<ComponentSample>? GetComponentSamplesFromFile(ILogger logger)
    {
        try
        {
            if (!File.Exists(ComponentSamplesFilePath))
                return null;

            using var stream = new FileStream(ComponentSamplesFilePath, FileMode.Open, FileAccess.Read);
            var componentSamples = JsonSerializer.Deserialize<List<ComponentSample>>(stream, JsonOptions);
            logger.Information("Component samples loaded from {FilePath}", ComponentSamplesFilePath);
            return componentSamples;
        }
        catch (Exception exception)
        {
            logger.Warning(exception, "Could not load Component samples from file");
            return null;
        }
    }

    public void WriteSamplesToFileIfNecessary()
    {
        if (!IsDirty)
        {
            Logger.Information("Component Samples did not change, saving them to {FilePath} is not necessary",
                               ComponentSamplesFilePath);
            return;
        }

        try
        {
            using var stream = new FileStream(ComponentSamplesFilePath, FileMode.Create);
            JsonSerializer.Serialize(stream, ComponentSamples, JsonOptions);
            Logger.Information("Component samples written to {FilePath}", ComponentSamplesFilePath);
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not save component samples");
        }
    }

    public void AddComponentSample(ComponentSample componentSample)
    {
        ComponentSamples.Add(componentSample);
        ComponentSamplesLookup.Add(componentSample.Id, componentSample);
    }

    public void RemoveComponentSample(ComponentSample componentSample)
    {
        ComponentSamplesLookup.Remove(componentSample.Id);
        ComponentSamples.Remove(componentSample);
    }
}