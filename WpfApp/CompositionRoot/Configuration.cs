using Microsoft.Extensions.Configuration;

namespace WpfApp.CompositionRoot;

public static class Configuration
{
    public static IConfiguration Create() =>
        new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false)
                                  .AddJsonFile("appsettings.Development.json", optional: true)
                                  .Build();
}