using System;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Backend.DataAccess;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        var now = new DateOnly(2023, 1, 1);
        return services.AddSingleton(sp => ContactsContext.CreateDefault(sp.GetRequiredService<ILogger>(),
                                                                         sp.GetRequiredService<Faker<Contact>>()))
                       .AddSingleton(new Faker<Contact>().UseSeed(42)
                                                         .StrictMode(true)
                                                         .RuleFor(c => c.Id, f => f.Random.Guid())
                                                         .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                                                         .RuleFor(c => c.LastName, f => f.Name.LastName())
                                                         .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                                                         .RuleFor(c => c.DateOfBirth,
                                                                  f => f.Date.BetweenDateOnly(now.AddYears(-60), now.AddYears(-18))));
    }
}