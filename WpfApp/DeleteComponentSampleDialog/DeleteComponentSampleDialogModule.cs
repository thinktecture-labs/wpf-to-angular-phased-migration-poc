using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.DeleteComponentSampleDialog;

public static class DeleteComponentSampleDialogModule
{
    public static IServiceCollection AddDeleteSampleDialog(this IServiceCollection services) =>
        services.AddSingleton<IShowConfirmDeletionDialogCommand, ShowConfirmDeletionDialogCommand>();
}