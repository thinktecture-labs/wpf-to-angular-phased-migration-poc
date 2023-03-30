using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;
using FuzzySharp;

namespace Backend.Contacts.GetContacts;

public sealed class InMemoryGetContactsUnitOfWork : IGetContactsUnitOfWork
{
    public InMemoryGetContactsUnitOfWork(ContactsContext context) => Context = context;

    private ContactsContext Context { get; }

    public Task<List<Contact>> GetContactsAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default)
    {
        var contacts = string.IsNullOrWhiteSpace(searchTerm) ?
            Context.Contacts :
            Process.ExtractSorted(new () { Email = searchTerm },
                                  Context.Contacts,
                                  c => c.Email,
                                  cutoff: 60)
                   .Select(r => r.Value);

        return Task.FromResult(contacts.Skip(skip)
                                       .Take(take)
                                       .ToList());
    }
}