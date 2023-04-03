using System;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp.ContactForm;

public static class ContactFormModule
{
    public static IServiceCollection AddContactForm(this IServiceCollection services) =>
        services.AddTransient<ContactFormViewModel>()
                .AddTransient<IContactFormSession, HttpContactFormSession>()
                .AddSingleton<Func<IContactFormSession>>(sp => sp.GetRequiredService<IContactFormSession>);
}