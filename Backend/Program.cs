using System;
using System.Threading.Tasks;
using Backend.CompositionRoot;
using Backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Backend;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = Logging.CreateLogger();
        try
        {
            var app = WebApplication.CreateBuilder(args)
                                    .ConfigureServices()
                                    .Build()
                                    .ConfigureHttpPipeline();

            var contactsContext = app.Services.GetRequiredService<ContactsContext>();
            await app.RunAsync();
            contactsContext.WriteContactsToFileIfNecessary();
            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Could not run backend");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}