using System;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public sealed class NavigateToFormCommand : INavigateToFormCommand
{
    public NavigateToFormCommand(Func<ComponentSample, ComponentSampleFormViewModel> createComponentSampleFormViewModel,
                                 INavigator navigator)
    {
        CreateComponentSampleFormViewModel = createComponentSampleFormViewModel;
        Navigator = navigator;
    }

    private Func<ComponentSample, ComponentSampleFormViewModel> CreateComponentSampleFormViewModel { get; }
    private INavigator Navigator { get; }

    public void Navigate(ComponentSample componentSample)
    {
        var viewModel = CreateComponentSampleFormViewModel(componentSample);
        var view = new ComponentSampleFormView(viewModel);
        Navigator.Show(view);
    }
}