using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.DeleteContact;

public sealed class InMemoryDeleteContactUnitOfWork : IDeleteContactUnitOfWork
{
    public InMemoryDeleteContactUnitOfWork(ContactsContext context) => Context = context;

    private ContactsContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default) =>
        Context.ContactsLookup.TryGetValue(id, out var contact) ?
            Task.FromResult<Contact?>(contact) :
            Task.FromResult<Contact?>(null);

    public void RemoveContact(Contact contact) => Context.RemoveContact(contact);
}