using System;
using System.Threading.Tasks;
using WpfApp.Shared;

namespace WpfApp.ContactForm;

public interface IContactFormSession : IDisposable
{
    Task CreateContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
}