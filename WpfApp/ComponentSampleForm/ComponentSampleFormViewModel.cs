using System;
using System.Collections.Generic;
using System.Globalization;
using Light.GuardClauses;
using Light.ViewModels;
using Serilog;
using WpfApp.ComponentSampleList;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public sealed class ComponentSampleFormViewModel : BaseNotifyDataErrorInfo
{
    private string _componentName;
    private bool _isInputEnabled = true;
    private string _migrationTimeText;
    private TimeSpan _parsedMigrationTime;
    private decimal _peakArea;

    public ComponentSampleFormViewModel(ComponentSample componentSample,
                                        List<ComponentSample> allSamples,
                                        INotificationPublisher notificationPublisher,
                                        INavigateToComponentSampleListCommand navigateToComponentSampleListCommand,
                                        Func<ISampleFormSession> createSession,
                                        ILogger logger)
    {
        _componentName = componentSample.ComponentName;
        _parsedMigrationTime = componentSample.MigrationTime;
        _migrationTimeText = componentSample.MigrationTime.ToString(@"hh\:mm\:ss");
        _peakArea = componentSample.PeakArea;

        ComponentSample = componentSample;
        AllSamples = allSamples;
        NotificationPublisher = notificationPublisher;
        NavigateToComponentSampleListCommand = navigateToComponentSampleListCommand;
        CreateSession = createSession;
        Logger = logger;

        CancelCommand = new (Cancel);
        SaveCommand = new (Save, () => !ValidationManager.HasErrors);
    }

    public ComponentSample ComponentSample { get; }
    public List<ComponentSample> AllSamples { get; }
    private INotificationPublisher NotificationPublisher { get; }
    private INavigateToComponentSampleListCommand NavigateToComponentSampleListCommand { get; }
    private Func<ISampleFormSession> CreateSession { get; }
    public ILogger Logger { get; }

    public string Title => ComponentSample.Id == Guid.Empty ? "Create Sample" : "Edit Sample";

    public string ComponentName
    {
        get => _componentName;
        set
        {
            if (!SetIfDifferent(ref _componentName, value))
                return;
            Validate(value, ValidateName);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string MigrationTimeText
    {
        get => _migrationTimeText;
        set
        {
            if (!SetIfDifferent(ref _migrationTimeText, value))
                return;

            ValidateAndParse(value, ValidateAndParseMigrationTime, out _parsedMigrationTime);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public decimal PeakArea
    {
        get => _peakArea;
        set
        {
            if (!SetIfDifferent(ref _peakArea, value))
                return;

            Validate(value, ValidatePeakArea);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public bool IsInputEnabled
    {
        get => _isInputEnabled;
        private set => SetIfDifferent(ref _isInputEnabled, value);
    }

    public DelegateCommand CancelCommand { get; }
    public DelegateCommand SaveCommand { get; }

    private static ValidationResult<string> ValidateName(string value)
    {
        if (value.IsNullOrWhiteSpace() || value.Length > 200)
            return "Please provide 1 to 200 characters";

        return ValidationResult<string>.Valid;
    }

    private static ValidationResult<string> ValidatePeakArea(decimal value) =>
        value is < 0m or > 10_000_000m ?
            "Peak Area must be between 0 and 10,000,000 RFU" :
            ValidationResult<string>.Valid;

    private static ValidationResult<string> ValidateAndParseMigrationTime(string text, out TimeSpan parsedTimeSpan)
    {
        if (TimeSpan.TryParse(text, CultureInfo.CurrentCulture, out parsedTimeSpan) &&
            parsedTimeSpan >= TimeSpan.FromSeconds(3) &&
            parsedTimeSpan <= TimeSpan.FromHours(1))
            return ValidationResult<string>.Valid;

        return "The migration time must be between 3 seconds and 1 hour";
    }

    private bool CheckForErrors()
    {
        Validate(_componentName, ValidateName);
        Validate(_peakArea, ValidatePeakArea);
        ValidateAndParse(_migrationTimeText, ValidateAndParseMigrationTime, out _parsedMigrationTime);
        SaveCommand.RaiseCanExecuteChanged();

        return HasErrors;
    }

    private void Cancel() => NavigateToComponentSampleListCommand.Navigate();

    private async void Save()
    {
        if (CheckForErrors())
            return;

        IsInputEnabled = false;

        ComponentSample.ComponentName = ComponentName;
        ComponentSample.MigrationTime = _parsedMigrationTime;
        ComponentSample.PeakArea = PeakArea;

        var isNewSample = ComponentSample.Id == Guid.Empty;
        try
        {
            using var session = CreateSession();

            if (isNewSample)
            {
                ComponentSample.Id = Guid.NewGuid();
                await session.CreateComponentSampleAsync(ComponentSample);
                NotificationPublisher.PublishNotification(
                    $"{ComponentSample.ComponentName} was created successfully",
                    NotificationLevel.Success
                );
            }
            else
            {
                await session.UpdateComponentSampleAsync(ComponentSample);
                NotificationPublisher.PublishNotification(
                    $"{ComponentSample.ComponentName} was updated successfully",
                    NotificationLevel.Success
                );
            }

            NavigateToComponentSampleListCommand.Navigate();
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not create or update sample");
            NotificationPublisher.PublishNotification(
                $"Could not {(isNewSample ? "create" : "update")} {ComponentSample.ComponentName}",
                NotificationLevel.Error
            );
        }
        finally
        {
            IsInputEnabled = true;
        }
    }
}