using System;
using WpfApp.Shared;

namespace WpfApp.DeleteContactDialog;

public sealed class ShowConfirmDeletionDialogCommand : IShowConfirmDeletionDialogCommand
{
    public ShowConfirmDeletionDialogCommand(Func<Contact, ConfirmDeletionViewModel> createViewModel,
                                            Func<MainWindow> resolveMainWindow)
    {
        CreateViewModel = createViewModel;
        ResolveMainWindow = resolveMainWindow;
    }

    private Func<Contact, ConfirmDeletionViewModel> CreateViewModel { get; }
    private Func<MainWindow> ResolveMainWindow { get; }

    public bool ShowDialog(Contact contact)
    {
        var viewModel = CreateViewModel(contact);
        var mainWindow = ResolveMainWindow();
        var view = new ConfirmDeletionWindow(viewModel){ Owner = mainWindow };
        return view.ShowDialog() == true;
    }
}