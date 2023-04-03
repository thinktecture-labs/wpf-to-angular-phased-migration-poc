using WpfApp.EndlessScrolling;
using WpfApp.Shared;

namespace WpfApp.ContactsList;

public interface IContactsSession : IPagingSession<Contact, ContactListFilters> { }