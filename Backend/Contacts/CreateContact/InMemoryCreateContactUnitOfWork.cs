using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.CreateContact;

public sealed class InMemoryCreateContactUnitOfWork : ICreateContactUnitOfWork
{
    public InMemoryCreateContactUnitOfWork(ContactsContext context) => Context = context;

    private ContactsContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public void AddContact(Contact contact) => Context.AddContact(contact);
}