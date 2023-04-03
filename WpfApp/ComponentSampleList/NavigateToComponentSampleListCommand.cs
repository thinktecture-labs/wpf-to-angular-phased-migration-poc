using System;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public sealed class NavigateToComponentSampleListCommand : INavigateToComponentSampleListCommand
{
    public NavigateToComponentSampleListCommand(INavigator navigator,
                                                Func<ComponentSampleListViewModel> createComponentSampleListViewModel)
    {
        Navigator = navigator;
        CreateComponentSampleListViewModel = createComponentSampleListViewModel;
    }

    private INavigator Navigator { get; }
    private Func<ComponentSampleListViewModel> CreateComponentSampleListViewModel { get; }

    public async void Navigate()
    {
        var viewModel = CreateComponentSampleListViewModel();
        var view = new ComponentSampleListView(viewModel);
        Navigator.Show(view);
        await viewModel.PagingViewModel.LoadNextPageAsync();
    }
}