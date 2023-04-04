using System;
using Light.GuardClauses;
using MahApps.Metro.Controls;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ConfirmDeletionWindow : MetroWindow
{
    public ConfirmDeletionWindow() => InitializeComponent();

    public ConfirmDeletionWindow(ComponentSample componentSample, INotificationPublisher notificationPublisher) : this()
    {
        ChromiumWebBrowser.Address = "http://localhost:5000/componentSamples/delete/" + componentSample.Id;
        ChromiumWebBrowser.JavascriptObjectRepository.ResolveObject += (_, e) =>
        {
            const string boundObjectName = "confirmDeletionBoundObject"; 
            if (e.ObjectName != boundObjectName)
                return;
        
            e.ObjectRepository.Register(boundObjectName, new ConfirmDeletionBoundObject(this, notificationPublisher));
        };
    }
}

public sealed class ConfirmDeletionBoundObject
{
    public ConfirmDeletionBoundObject(ConfirmDeletionWindow window, INotificationPublisher notificationPublisher)
    {
        Window = window;
        NotificationPublisher = notificationPublisher;
    }

    private ConfirmDeletionWindow Window { get; }
    private INotificationPublisher NotificationPublisher { get; }

    // This method is not called on the UI thread, we need to dispatch all interactions
    // to it via the Window.
    // ReSharper disable once UnusedMember.Global -- this method is called from the Angular app
    public void CloseDialog(string message, int level)
    {
        Window.Dispatcher.BeginInvoke(new Action(() =>
        {
            var notificationLevel = (NotificationLevel) level;
            Window.DialogResult = notificationLevel == NotificationLevel.Success;
            if (!message.IsNullOrWhiteSpace())
                NotificationPublisher.PublishNotification(message, notificationLevel);
            Window.Close();
        }));
    }
}

