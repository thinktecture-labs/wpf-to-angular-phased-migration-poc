using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CefSharp;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ComponentSampleFormView : UserControl
{
    public ComponentSampleFormView() => InitializeComponent();

    public ComponentSampleFormView(ComponentSampleFormViewModel viewModel) : this()
    {
        DataContext = viewModel;
        ChromiumWebBrowser.JavascriptObjectRepository.ResolveObject += (_, e) =>
        {
            const string boundObjectName = "chartBoundObject";
            if (e.ObjectName == boundObjectName)
            {
                var boundObject = new ChartBoundObject(viewModel.AllSamples,
                                                       viewModel.ComponentSample,
                                                       viewModel.Logger);
                e.ObjectRepository.Register(boundObjectName, boundObject, BindingOptions.DefaultBinder);
            }
        };
    }
}

public sealed class ChartBoundObject
{
    public ChartBoundObject(List<ComponentSample> componentSamples,
                            ComponentSample currentSample,
                            ILogger logger)
    {
        ComponentSamples = componentSamples;
        CurrentSample = currentSample;
        Logger = logger;
    }

    private List<ComponentSample> ComponentSamples { get; }
    private ComponentSample CurrentSample { get; }
    private ILogger Logger { get; }

    // ReSharper disable once UnusedMember.Global -- this method is called from Angular
    public async void RegisterSetChartData(IJavascriptCallback setChartData)
    {
        try
        {
            var allSamples = ComponentSamples.Select(ComponentSampleDto.FromComponentSample)
                                             .ToList();
            var currentSample = ComponentSampleDto.FromComponentSample(CurrentSample);
            await setChartData.ExecuteAsync(allSamples, currentSample);
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not set chart data");
        }
    }
}