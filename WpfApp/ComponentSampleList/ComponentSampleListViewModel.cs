using System;
using Light.ViewModels;
using Serilog;
using WpfApp.ComponentSampleForm;
using WpfApp.DeleteComponentSampleDialog;
using WpfApp.EndlessScrolling;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public sealed class ComponentSampleListViewModel : BaseNotifyPropertyChanged, IHasPagingViewModel
{
    private string _searchTerm = string.Empty;
    private ComponentSample? _selectedSample;

    public ComponentSampleListViewModel(Func<IComponentSamplesSession> createSession,
                                        INotificationPublisher notificationPublisher,
                                        INavigateToFormCommand navigateToFormCommand,
                                        IShowConfirmDeletionDialogCommand showConfirmDeletionDialogCommand,
                                        ILogger logger)
    {
        NavigateToFormCommand = navigateToFormCommand;
        ShowConfirmDeletionDialogCommand = showConfirmDeletionDialogCommand;
        PagingViewModel = new (createSession, 30, new (string.Empty), notificationPublisher, logger);

        CreateSampleCommand = new (CreateSample);
        EditSampleCommand = new (EditSample, () => SelectedSample is not null);
        DeleteSampleCommand = new (DeleteSample, () => SelectedSample is not null);
    }

    public PagingViewModel<ComponentSample, SampleListFilters> PagingViewModel { get; }
    private INavigateToFormCommand NavigateToFormCommand { get; }
    private IShowConfirmDeletionDialogCommand ShowConfirmDeletionDialogCommand { get; }

    public ComponentSample? SelectedSample
    {
        get => _selectedSample;
        set
        {
            if (!SetIfDifferent(ref _selectedSample, value))
                return;

            EditSampleCommand.RaiseCanExecuteChanged();
            DeleteSampleCommand.RaiseCanExecuteChanged();
        }
    }

    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (SetIfDifferent(ref _searchTerm, value))
                PagingViewModel.Filters = new (value);
        }
    }

    public DelegateCommand CreateSampleCommand { get; }
    public DelegateCommand EditSampleCommand { get; }
    public DelegateCommand DeleteSampleCommand { get; }

    IPagingViewModel IHasPagingViewModel.PagingViewModel => PagingViewModel;

    private void CreateSample() => NavigateToFormCommand.Navigate(new ());

    private void EditSample()
    {
        if (SelectedSample is not null)
            NavigateToFormCommand.Navigate(SelectedSample);
    }

    private async void DeleteSample()
    {
        if (SelectedSample is null)
            return;

        var wasSampleDeleted = ShowConfirmDeletionDialogCommand.ShowDialog(SelectedSample);
        if (wasSampleDeleted)
            await PagingViewModel.ReloadAsync();
    }
}