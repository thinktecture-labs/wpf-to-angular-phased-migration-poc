using System;
using Light.ViewModels;
using Serilog;
using WpfApp.ComponentSampleForm;
using WpfApp.DeleteComponentSampleDialog;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public sealed class ComponentSampleListViewModel : BaseNotifyPropertyChanged
{
    private string? _searchTerm = string.Empty;
    private ComponentSample? _selectedSample;

    public ComponentSampleListViewModel(Func<IComponentSamplesSession> createSession,
                                        INotificationPublisher notificationPublisher,
                                        INavigateToFormCommand navigateToFormCommand,
                                        IShowConfirmDeletionDialogCommand showConfirmDeletionDialogCommand,
                                        ILogger logger)
    {
        CreateSession = createSession;
        NotificationPublisher = notificationPublisher;
        NavigateToFormCommand = navigateToFormCommand;
        ShowConfirmDeletionDialogCommand = showConfirmDeletionDialogCommand;
        Logger = logger;
        BoundObject = new (this, logger);

        CreateSampleCommand = new (CreateSample);
        EditSampleCommand = new (EditSample, () => SelectedSample is not null);
        DeleteSampleCommand = new (DeleteSample, () => SelectedSample is not null);
    }

    private Func<IComponentSamplesSession> CreateSession { get; }
    private INotificationPublisher NotificationPublisher { get; }
    private INavigateToFormCommand NavigateToFormCommand { get; }
    private IShowConfirmDeletionDialogCommand ShowConfirmDeletionDialogCommand { get; }
    private ILogger Logger { get; }
    public SampleListBoundObject BoundObject { get; }

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

    public string? SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (SetIfDifferent(ref _searchTerm, value))
                BoundObject.SetSearchTerm(value);
        }
    }

    public DelegateCommand CreateSampleCommand { get; }
    public DelegateCommand EditSampleCommand { get; }
    public DelegateCommand DeleteSampleCommand { get; }

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
            await BoundObject.ReloadAsync();
    }

    public async void OnSelectedSampleChanged(string sampleId)
    {
        if (!Guid.TryParse(sampleId, out var parsedId))
        {
            SelectedSample = null;
            return;
        }

        try
        {
            using var session = CreateSession();
            SelectedSample = await session.GetComponentSampleAsync(parsedId);
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not load sample");
            NotificationPublisher.PublishNotification(
                "Could not load sample",
                NotificationLevel.Error
            );
        }
    }
}