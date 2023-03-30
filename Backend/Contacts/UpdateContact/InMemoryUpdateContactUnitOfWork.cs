using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.UpdateContact;

public sealed class InMemoryUpdateContactUnitOfWork : IUpdateContactUnitOfWork
{
    public InMemoryUpdateContactUnitOfWork(ContactsContext context) => Context = context;

    private ContactsContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default) =>
        Context.ContactsLookup.TryGetValue(contactId, out var contact) ?
            Task.FromResult<Contact?>(contact) :
            Task.FromResult<Contact?>(null);
}