using Microsoft.Extensions.DependencyInjection;

namespace Backend.Contacts.CreateContact;

public static class CreateContactModule
{
    public static IServiceCollection AddCreateContactEndpoint(this IServiceCollection services) =>
        services.AddSingleton<CreateContactDtoValidator>()
                .AddScoped<ICreateContactUnitOfWork, InMemoryCreateContactUnitOfWork>();
}