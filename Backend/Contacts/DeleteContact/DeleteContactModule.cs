using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts.DeleteContact;

public static class DeleteContactModule
{
    public static IServiceCollection AddDeleteContactEndpoint(this IServiceCollection services) =>
        services.AddScoped<IDeleteContactUnitOfWork, InMemoryDeleteContactUnitOfWork>();
}