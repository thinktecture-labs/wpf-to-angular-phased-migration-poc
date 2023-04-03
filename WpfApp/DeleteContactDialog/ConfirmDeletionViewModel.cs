using System;
using System.Threading.Tasks;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteContactDialog;

public sealed class ConfirmDeletionViewModel
{
    public ConfirmDeletionViewModel(Contact contact,
                                    Func<IDeleteContactSession> createSession,
                                    INotificationPublisher notificationPublisher,
                                    ILogger logger)
    {
        Contact = contact;
        CreateSession = createSession;
        NotificationPublisher = notificationPublisher;
        Logger = logger;
    }

    public Contact Contact { get; }
    private Func<IDeleteContactSession> CreateSession { get; }
    private INotificationPublisher NotificationPublisher { get; }
    private ILogger Logger { get; }

    public async Task<bool> DeleteContactAsync()
    {
        try
        {
            using var session = CreateSession();
            await session.DeleteContactAsync(Contact.Id);
            NotificationPublisher.PublishNotification(
                $"{Contact.FirstName} {Contact.LastName} was deleted successfully"
            );
            return true;
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not delete contact");
            NotificationPublisher.PublishNotification(
                $"Could not delete {Contact.FirstName} {Contact.LastName}",
                NotificationLevel.Error
            );
            return false;
        }
    }
}