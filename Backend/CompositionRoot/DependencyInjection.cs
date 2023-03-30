using Backend.Contacts;
using Backend.DataAccess;
using Backend.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.CompositionRoot;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.UseSerilog();

        builder.Services
               .AddLightValidation()
               .AddDataAccess()
               .AddContactEndpoints()
               .AddHealthChecks();
        return builder;
    }
}