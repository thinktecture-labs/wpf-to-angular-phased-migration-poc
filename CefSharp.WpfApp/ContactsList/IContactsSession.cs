using CefSharp.WpfApp.EndlessScrolling;
using CefSharp.WpfApp.Shared;

namespace CefSharp.WpfApp.ContactsList;

public interface IContactsSession : IPagingSession<Contact, ContactListFilters> { }