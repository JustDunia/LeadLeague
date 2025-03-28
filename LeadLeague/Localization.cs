using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace LeadLeague;

public static class Localization
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddRequestLocalization(options =>
         {
             List<CultureInfo> supportedCultures =
             [
                 new CultureInfo("pl"),
                 new CultureInfo("en-US")
             ];

             options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
             options.SupportedCultures = supportedCultures;
             options.SupportedUICultures = supportedCultures;
         });

        return services;
    }
}
