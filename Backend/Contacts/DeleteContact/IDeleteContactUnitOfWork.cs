using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.Contacts.DeleteContact;

public interface IDeleteContactUnitOfWork : IUnitOfWork
{
    Task<Contact?> GetContactAsync(Guid id, CancellationToken cancellationToken = default);
    void RemoveContact(Contact contact);
}