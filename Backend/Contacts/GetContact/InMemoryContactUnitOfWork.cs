using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.GetContact;

public sealed class InMemoryContactUnitOfWork : IGetContactUnitOfWork
{
    public InMemoryContactUnitOfWork(ContactsContext context) => Context = context;

    private ContactsContext Context { get; }

    public Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default) =>
        Context.ContactsLookup.TryGetValue(contactId, out var contact) ?
            Task.FromResult<Contact?>(contact) :
            Task.FromResult<Contact?>(null);
}