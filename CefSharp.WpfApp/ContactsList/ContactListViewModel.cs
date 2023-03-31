using System;
using CefSharp.WpfApp.EndlessScrolling;
using CefSharp.WpfApp.Shared;
using Light.ViewModels;
using Serilog;

namespace CefSharp.WpfApp.ContactsList;

public sealed class ContactListViewModel : BaseNotifyPropertyChanged, IHasPagingViewModel
{
    private string _searchTerm = string.Empty;

    public ContactListViewModel(Func<IContactsSession> createSession,
                                ILogger logger) =>
        PagingViewModel = new (createSession, 30, new (string.Empty), logger);

    public PagingViewModel<Contact, ContactListFilters> PagingViewModel { get; }

    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (SetIfDifferent(ref _searchTerm, value))
                PagingViewModel.Filters = new (value);
        }
    }

    IPagingViewModel IHasPagingViewModel.PagingViewModel => PagingViewModel;
}