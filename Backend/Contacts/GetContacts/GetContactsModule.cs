using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts.GetContacts;

public static class GetContactsModule
{
    public static IServiceCollection AddGetContactsEndpoint(this IServiceCollection services) =>
        services.AddScoped<IGetContactsUnitOfWork, InMemoryGetContactsUnitOfWork>();
}