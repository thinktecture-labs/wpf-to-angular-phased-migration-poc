using Backend.DataAccess;

namespace Backend.Contacts.CreateContact;

public interface ICreateContactUnitOfWork : IUnitOfWork
{
    void AddContact(Contact contact);
}