using System;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Backend.DataAccess;

public static class DataAccessModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        var faker =
            new Faker<ComponentSample>()
               .UseSeed(42)
               .RuleFor(c => c.Id, f => f.Random.Guid())
               .RuleFor(c => c.MigrationTime, f => f.Date.Timespan(TimeSpan.FromHours(1)) + TimeSpan.FromSeconds(3))
               .RuleFor(c => c.PeakArea, f => f.Random.Decimal(0m, 10_000_000m));
        
        return services.AddSingleton(
                            sp => ComponentSampleContext.CreateDefault(
                                sp.GetRequiredService<ILogger>(),
                                sp.GetRequiredService<Faker<ComponentSample>>()
                            )
                        )
                       .AddSingleton(faker);
    }
}