using System;
using System.Threading.Tasks;
using CefSharp.WpfApp.Shared;

namespace CefSharp.WpfApp.ContactForm;

public interface IContactFormSession : IDisposable
{
    Task CreateContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
}