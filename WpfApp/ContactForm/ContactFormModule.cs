using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.ContactsList;
using WpfApp.Shared;

namespace WpfApp.ContactForm;

public static class ContactFormModule
{
    public static IServiceCollection AddContactForm(this IServiceCollection services) =>
        services.AddTransient<ContactFormViewModel>()
                .AddSingleton<Func<Contact, ContactFormViewModel>>(sp =>
                 {
                     return contact => new (contact,
                                            sp.GetRequiredService<INotificationPublisher>(),
                                            sp.GetRequiredService<INavigateToContactsListCommand>(),
                                            sp.GetRequiredService<Func<IContactFormSession>>(),
                                            sp.GetRequiredService<ILogger>());
                 })
                .AddTransient<IContactFormSession, HttpContactFormSession>()
                .AddSingleton<Func<IContactFormSession>>(sp => sp.GetRequiredService<IContactFormSession>)
                .AddSingleton<INavigateToContactFormCommand, NavigateToContactFormCommand>();
}