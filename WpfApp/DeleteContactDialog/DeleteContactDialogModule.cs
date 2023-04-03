using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteContactDialog;

public static class DeleteContactDialogModule
{
    public static IServiceCollection AddDeleteContactDialog(this IServiceCollection services) =>
        services.AddTransient<IDeleteContactSession, HttpDeleteContactSession>()
                .AddSingleton<Func<IDeleteContactSession>>(sp => sp.GetRequiredService<IDeleteContactSession>)
                .AddTransient<ConfirmDeletionViewModel>()
                .AddSingleton<Func<Contact, ConfirmDeletionViewModel>>(sp =>
                 {
                     return contact => new (contact,
                                            sp.GetRequiredService<Func<IDeleteContactSession>>(),
                                            sp.GetRequiredService<INotificationPublisher>(),
                                            sp.GetRequiredService<ILogger>());
                 })
                .AddSingleton<IShowConfirmDeletionDialogCommand, ShowConfirmDeletionDialogCommand>();
}