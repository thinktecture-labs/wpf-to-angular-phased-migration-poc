using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using CefSharp;
using Serilog;

namespace WpfApp.ComponentSampleList;

public sealed class SampleListBoundObject
{
    public SampleListBoundObject(ComponentSampleListViewModel viewModel,
                                 ILogger logger)
    {
        ViewModel = viewModel;
        Logger = logger;
    }

    private ComponentSampleListViewModel ViewModel { get; }
    private ILogger Logger { get; }
    public Dispatcher? Dispatcher { get; set; }
    private IJavascriptCallback? SetSearchTermCallback { get; set; }
    private IJavascriptCallback? ReloadCallback { get; set; }

    // ReSharper disable once UnusedMember.Global -- this method is called from Js/Ts
    public void RegisterSetSearchTerm(IJavascriptCallback setSearchTerm) => SetSearchTermCallback = setSearchTerm;
    
    // ReSharper disable once UnusedMember.Global -- this method is called from Js/Ts
    public void RegisterReload(IJavascriptCallback reload) => ReloadCallback = reload;

    // ReSharper disable once UnusedMember.Global -- this method is called from Js/Ts
    public void SelectedSampleChanged(string sampleId)
    {
        if (Dispatcher is null)
        {
            ViewModel.OnSelectedSampleChanged(sampleId);
        }
        else
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                   new Action<string>(ViewModel.OnSelectedSampleChanged),
                                   sampleId);
        }
    }

    public async void SetSearchTerm(string searchTerm)
    {
        if (SetSearchTermCallback is null)
            throw new InvalidOperationException("You must bind this object before setting the search term");

        try
        {
            await SetSearchTermCallback.ExecuteAsync(searchTerm);
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not set search term {SearchTerm}", searchTerm);
        }
    }

    public async Task ReloadAsync()
    {
        if (ReloadCallback is null)
            throw new InvalidOperationException("You must bind this object before calling reload");

        try
        {
            await ReloadCallback.ExecuteAsync();
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not reload");
        }
    }
}