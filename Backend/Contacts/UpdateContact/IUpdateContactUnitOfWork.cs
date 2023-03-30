using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.UpdateContact;

public interface IUpdateContactUnitOfWork : IUnitOfWork
{
    Task<Contact?> GetContactAsync(Guid contactId, CancellationToken cancellationToken = default);
}