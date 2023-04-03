using System;
using Light.ViewModels;
using Serilog;
using WpfApp.DeleteContactDialog;
using WpfApp.EndlessScrolling;
using WpfApp.Shared;

namespace WpfApp.ContactsList;

public sealed class ContactListViewModel : BaseNotifyPropertyChanged, IHasPagingViewModel
{
    private string _searchTerm = string.Empty;
    private Contact? _selectedContact;

    public ContactListViewModel(Func<IContactsSession> createSession,
                                IShowConfirmDeletionDialogCommand showConfirmDeletionDialogCommand,
                                ILogger logger)
    {
        ShowConfirmDeletionDialogCommand = showConfirmDeletionDialogCommand;
        PagingViewModel = new (createSession, 30, new (string.Empty), logger);

        EditContactCommand = new (EditContact, () => SelectedContact is not null);
        DeleteContactCommand = new (DeleteContact, () => SelectedContact is not null);
    }

    public PagingViewModel<Contact, ContactListFilters> PagingViewModel { get; }
    private IShowConfirmDeletionDialogCommand ShowConfirmDeletionDialogCommand { get; }

    public Contact? SelectedContact
    {
        get => _selectedContact;
        set
        {
            if (!SetIfDifferent(ref _selectedContact, value))
                return;
            
            EditContactCommand.RaiseCanExecuteChanged();
            DeleteContactCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand EditContactCommand { get; }
    public DelegateCommand DeleteContactCommand { get; }

    IPagingViewModel IHasPagingViewModel.PagingViewModel => PagingViewModel;

    private void EditContact()
    {
            
    }

    private async void DeleteContact()
    {
        if (SelectedContact is null)
            return;
        
        var wasContactDeleted = ShowConfirmDeletionDialogCommand.ShowDialog(SelectedContact);
        if (wasContactDeleted)
            await PagingViewModel.ReloadAsync();
    }
}