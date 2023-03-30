using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts.UpdateContact;

public static class UpdateContactModule
{
    public static IServiceCollection AddUpdateContactEndpoint(this IServiceCollection services) =>
        services.AddSingleton<UpdateContactValidator>()
                .AddScoped<IUpdateContactUnitOfWork, InMemoryUpdateContactUnitOfWork>();
}