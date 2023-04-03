using System;
using Light.GuardClauses;
using Light.ViewModels;
using Serilog;
using WpfApp.ContactsList;
using WpfApp.Shared;

namespace WpfApp.ContactForm;

public sealed class ContactFormViewModel : BaseNotifyDataErrorInfo
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private bool _isInputEnabled = true;
    private DateTime? _dateOfBirth;

    public ContactFormViewModel(Contact contact,
                                INavigateToContactsListCommand navigateToContactsListCommand,
                                Func<IContactFormSession> createSession,
                                ILogger logger)
    {
        _firstName = contact.FirstName;
        _lastName = contact.LastName;
        _email = contact.Email;
        _dateOfBirth = contact.DateOfBirth.ToDateTime(new ());

        Contact = contact;
        NavigateToContactsListCommand = navigateToContactsListCommand;
        CreateSession = createSession;
        Logger = logger;

        CancelCommand = new (Cancel);
        SaveCommand = new (Save, () => !ValidationManager.HasErrors);
    }
    
    private Contact Contact { get; }
    private INavigateToContactsListCommand NavigateToContactsListCommand { get; }
    private Func<IContactFormSession> CreateSession { get; }
    private ILogger Logger { get; }

    public string Title => Contact.Id == Guid.Empty ? "Create Contact" : "Edit Contact";

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (!SetIfDifferent(ref _firstName, value))
                return;
            Validate(value, ValidateName);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (!SetIfDifferent(ref _lastName, value))
                return;
            
            Validate(value, ValidateName);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (!SetIfDifferent(ref _email, value))
                return;
            
            Validate(value, ValidateEmail);
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public DateTime? DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (!SetIfDifferent(ref _dateOfBirth, value))
                return;
            
            Validate(value, ValidateDateOfBirth);
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

    private static ValidationResult<string> ValidateEmail(string value)
    {
        if (value.IsNullOrWhiteSpace() || value.Length < 3)
            goto Failed;

        var indexOfAtSign = value.IndexOf('@');
        if (indexOfAtSign is -1 or 0 || indexOfAtSign == value.Length - 1)
            goto Failed;

        var lastIndexOfAtSign = value.LastIndexOf('@');
        if (indexOfAtSign != lastIndexOfAtSign)
            goto Failed;
        
        return ValidationResult<string>.Valid;
        
        Failed:
        return "Please provide a valid email address";
    }

    private static ValidationResult<string> ValidateDateOfBirth(DateTime? dateOfBirth)
    {
        if (dateOfBirth is null)
            return "Please provide a date of birth;";

        var dateOnly = DateOnly.FromDateTime(dateOfBirth.Value);
        if (dateOnly < new DateOnly(1900, 1, 1))
            return "The date of birth must be later than 1900-01-01";

        var now = DateOnly.FromDateTime(DateTime.Today);
        var eighteenYearsAgo = now.AddYears(-18);
        if (dateOnly > eighteenYearsAgo)
            return "The contact must be at least 18 years old";
        
        return ValidationResult<string>.Valid;
    }

    private bool CheckForErrors()
    {
        Validate(_firstName, ValidateName);
        Validate(_lastName, ValidateName);
        Validate(_email, ValidateEmail);
        Validate(_dateOfBirth, ValidateDateOfBirth);
        SaveCommand.RaiseCanExecuteChanged();

        return HasErrors;
    }

    private void Cancel() => NavigateToContactsListCommand.Navigate();

    private async void Save()
    {
        if (CheckForErrors())
            return;

        IsInputEnabled = false;

        Contact.FirstName = FirstName;
        Contact.LastName = LastName;
        Contact.Email = Email;
        Contact.DateOfBirth = DateOnly.FromDateTime(_dateOfBirth!.Value);

        try
        {
            using var session = CreateSession();

            if (Contact.Id == Guid.Empty)
            {
                Contact.Id = Guid.NewGuid();
                await session.CreateContactAsync(Contact);
            }
            else
            {
                await session.UpdateContactAsync(Contact);
            }

            NavigateToContactsListCommand.Navigate();
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not create or update contact");
        }
        finally
        {
            IsInputEnabled = true;
        }
    }
}