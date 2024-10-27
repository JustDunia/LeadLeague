using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LeadLeague;

public static class AuthConfiguration
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services)
    {
        var oktaDomain = Environment.GetEnvironmentVariable("OKTA_DOMAIN");
        var oktaAudience = Environment.GetEnvironmentVariable("OKTA_AUDIENCE");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{oktaDomain}";
            options.Audience = oktaAudience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            };
        });

        services.AddAuthorization();

        return services;
    }
}
