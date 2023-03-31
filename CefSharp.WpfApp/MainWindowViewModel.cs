using CefSharp.WpfApp.Shared;
using Light.ViewModels;

namespace CefSharp.WpfApp;

public sealed class MainWindowViewModel : BaseNotifyPropertyChanged, INavigator
{
    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        private set => SetIfDifferent(ref _currentView, value);
    }

    public void Show(object view) => CurrentView = view;
}