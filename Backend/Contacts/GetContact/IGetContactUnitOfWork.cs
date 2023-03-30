using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.GetContact;

public interface IGetContactUnitOfWork
{
    Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default);
}