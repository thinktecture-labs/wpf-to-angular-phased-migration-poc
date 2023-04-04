using System;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public sealed class ShowConfirmDeletionDialogCommand : IShowConfirmDeletionDialogCommand
{
    public ShowConfirmDeletionDialogCommand(Func<INotificationPublisher> resolveNotificationPublisher,
                                            Func<MainWindow> resolveMainWindow)
    {
        ResolveNotificationPublisher = resolveNotificationPublisher;
        ResolveMainWindow = resolveMainWindow;
    }

    private Func<INotificationPublisher> ResolveNotificationPublisher { get; }
    private Func<MainWindow> ResolveMainWindow { get; }

    public bool ShowDialog(ComponentSample componentSample)
    {
        var notificationPublisher = ResolveNotificationPublisher();
        var mainWindow = ResolveMainWindow();
        var view = new ConfirmDeletionWindow(componentSample, notificationPublisher){ Owner = mainWindow };
        return view.ShowDialog() == true;
    }
}