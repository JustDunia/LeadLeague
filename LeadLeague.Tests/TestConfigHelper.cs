using Microsoft.Extensions.Configuration;

namespace LeadLeague.Tests;
public class TestConfigHelper
{
    public static IConfigurationRoot GetIConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
