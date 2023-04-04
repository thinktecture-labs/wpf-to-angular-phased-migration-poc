using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public sealed class ConfirmDeletionViewModel
{
    public ConfirmDeletionViewModel(ComponentSample componentSample,
                                    INotificationPublisher notificationPublisher,
                                    ILogger logger)
    {
        ComponentSample = componentSample;
        NotificationPublisher = notificationPublisher;
        Logger = logger;
        Url = "http://localhost:5000/componentSamples/delete";
    }

    public ComponentSample ComponentSample { get; }
    public string Url { get; }
    private INotificationPublisher NotificationPublisher { get; }
    private ILogger Logger { get; }
}