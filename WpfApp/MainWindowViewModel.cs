using Light.ViewModels;
using WpfApp.Shared;

namespace WpfApp;

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