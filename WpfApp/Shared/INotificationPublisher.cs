namespace WpfApp.Shared;

public interface INotificationPublisher
{
    void PublishNotification(string message, NotificationLevel level = NotificationLevel.Info);
}