using System;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public sealed class ShowConfirmDeletionDialogCommand : IShowConfirmDeletionDialogCommand
{
    public ShowConfirmDeletionDialogCommand(Func<ComponentSample, ConfirmDeletionViewModel> createViewModel,
                                            Func<MainWindow> resolveMainWindow)
    {
        CreateViewModel = createViewModel;
        ResolveMainWindow = resolveMainWindow;
    }

    private Func<ComponentSample, ConfirmDeletionViewModel> CreateViewModel { get; }
    private Func<MainWindow> ResolveMainWindow { get; }

    public bool ShowDialog(ComponentSample componentSample)
    {
        var viewModel = CreateViewModel(componentSample);
        var mainWindow = ResolveMainWindow();
        var view = new ConfirmDeletionWindow(viewModel){ Owner = mainWindow };
        return view.ShowDialog() == true;
    }
}