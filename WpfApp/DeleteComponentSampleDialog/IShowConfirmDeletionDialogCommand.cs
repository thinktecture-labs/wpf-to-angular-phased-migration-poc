using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public interface IShowConfirmDeletionDialogCommand
{
    bool ShowDialog(ComponentSample componentSample);
}