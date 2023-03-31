using System;
using Microsoft.Extensions.DependencyInjection;

namespace CefSharp.WpfApp.ContactsList;

public static class ContactListModule 
{
    public static IServiceCollection AddContactList(this IServiceCollection services) =>
        services.AddTransient<ContactListViewModel>()
                .AddTransient<Func<ContactListViewModel>>(sp => sp.GetRequiredService<ContactListViewModel>)
                .AddTransient<IContactsSession, HttpContactsSession>()
                .AddSingleton<Func<IContactsSession>>(sp => sp.GetRequiredService<IContactsSession>)
                .AddSingleton<NavigateToContactsListCommand>();
}