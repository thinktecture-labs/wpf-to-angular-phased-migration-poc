using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public sealed class NavigateToFormCommand : INavigateToFormCommand
{
    public NavigateToFormCommand(
        Func<ComponentSample, List<ComponentSample>, ComponentSampleFormViewModel> createComponentSampleFormViewModel,
        IHttpClientFactory httpClientFactory,
        INavigator navigator
    )
    {
        CreateComponentSampleFormViewModel = createComponentSampleFormViewModel;
        HttpClientFactory = httpClientFactory;
        Navigator = navigator;
    }

    private Func<ComponentSample, List<ComponentSample>, ComponentSampleFormViewModel>
        CreateComponentSampleFormViewModel { get; }

    private IHttpClientFactory HttpClientFactory { get; }

    private INavigator Navigator { get; }

    public async void Navigate(ComponentSample componentSample)
    {
        using var httpClient = HttpClientFactory.CreateClient(HttpConstants.BackendHttpClientName);
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters.Add("skip", "0");
        queryParameters.Add("take", "100");
        var relativeUrl = "/api/componentSamples?" + queryParameters;
        var allSamples = await httpClient.GetFromJsonAsync<List<ComponentSample>>(relativeUrl, Json.DefaultOptions);
        
        var viewModel = CreateComponentSampleFormViewModel(componentSample, allSamples!);
        var view = new ComponentSampleFormView(viewModel);
        Navigator.Show(view);
    }
}