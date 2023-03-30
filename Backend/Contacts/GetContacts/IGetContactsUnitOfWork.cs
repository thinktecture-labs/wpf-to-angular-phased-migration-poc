using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.GetContacts;

public interface IGetContactsUnitOfWork
{
    Task<List<Contact>> GetContactsAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default);
}