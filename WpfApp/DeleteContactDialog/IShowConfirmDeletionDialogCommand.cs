using WpfApp.Shared;

namespace WpfApp.DeleteContactDialog;

public interface IShowConfirmDeletionDialogCommand
{
    bool ShowDialog(Contact contact);
}