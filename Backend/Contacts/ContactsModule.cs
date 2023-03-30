using Backend.Contacts.CreateContact;
using Backend.Contacts.DeleteContact;
using Backend.Contacts.GetContact;
using Backend.Contacts.GetContacts;
using Backend.Contacts.UpdateContact;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts;

public static class ContactsModule
{
    public static IServiceCollection AddContactEndpoints(this IServiceCollection services) => services.AddGetContactsEndpoint()
                                                                                                      .AddGetContactEndpoint()
                                                                                                      .AddCreateContactEndpoint()
                                                                                                      .AddUpdateContactEndpoint()
                                                                                                      .AddDeleteContactEndpoint();

    public static WebApplication MapContactEndpoints(this WebApplication app) => app.MapGetContacts()
                                                                                    .MapGetContact()
                                                                                    .MapCreateContact()
                                                                                    .MapUpdateContact()
                                                                                    .MapDeleteContact();
}