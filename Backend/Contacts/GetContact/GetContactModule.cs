using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts.GetContact;

public static class GetContactModule
{
    public static IServiceCollection AddGetContactEndpoint(this IServiceCollection services) =>
        services.AddScoped<IGetContactUnitOfWork, InMemoryContactUnitOfWork>(); 
}