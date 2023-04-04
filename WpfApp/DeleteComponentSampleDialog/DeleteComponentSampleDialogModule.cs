using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteComponentSampleDialog;

public static class DeleteComponentSampleDialogModule
{
    public static IServiceCollection AddDeleteSampleDialog(this IServiceCollection services) =>
        services.AddTransient<ConfirmDeletionViewModel>()
                .AddSingleton<Func<ComponentSample, ConfirmDeletionViewModel>>(sp =>
                 {
                     return sample => new (sample,
                                           sp.GetRequiredService<INotificationPublisher>(),
                                           sp.GetRequiredService<ILogger>());
                 })
                .AddSingleton<IShowConfirmDeletionDialogCommand, ShowConfirmDeletionDialogCommand>();
}