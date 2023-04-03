using System;
using System.Diagnostics;
using Light.ViewModels;
using MaterialDesignThemes.Wpf;
using WpfApp.Shared;

namespace WpfApp;

public sealed class MainWindowViewModel : BaseNotifyPropertyChanged,
                                          INavigator,
                                          INotificationPublisher
{
    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        private set => SetIfDifferent(ref _currentView, value);
    }

    public SnackbarMessageQueue SnackbarMessageQueue { get; } = new (TimeSpan.FromSeconds(7));

    public void Show(object view) => CurrentView = view;
    
    public void PublishNotification(string message, NotificationLevel level = NotificationLevel.Info)
    {
        if (level == NotificationLevel.Error)
            SnackbarMessageQueue.Enqueue(message, "Show Log", OpenLog);
        else
            SnackbarMessageQueue.Enqueue(message);
    }

    private static void OpenLog()
    {
        using var process = Process.Start("notepad.exe", "WpfApp.log");
    }
}