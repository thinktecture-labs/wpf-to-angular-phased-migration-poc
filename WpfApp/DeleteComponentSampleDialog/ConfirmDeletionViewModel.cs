using System;
using System.Threading.Tasks;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public sealed class ConfirmDeletionViewModel
{
    public ConfirmDeletionViewModel(ComponentSample componentSample,
                                    Func<IDeleteSampleSession> createSession,
                                    INotificationPublisher notificationPublisher,
                                    ILogger logger)
    {
        ComponentSample = componentSample;
        CreateSession = createSession;
        NotificationPublisher = notificationPublisher;
        Logger = logger;
    }

    public ComponentSample ComponentSample { get; }
    private Func<IDeleteSampleSession> CreateSession { get; }
    private INotificationPublisher NotificationPublisher { get; }
    private ILogger Logger { get; }

    public async Task<bool> DeleteSampleAsync()
    {
        try
        {
            using var session = CreateSession();
            await session.DeleteSampleAsync(ComponentSample.Id);
            NotificationPublisher.PublishNotification(
                $"{ComponentSample.ComponentName} was deleted successfully"
            );
            return true;
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not delete sample");
            NotificationPublisher.PublishNotification(
                $"Could not delete {ComponentSample.ComponentName}",
                NotificationLevel.Error
            );
            return false;
        }
    }
}