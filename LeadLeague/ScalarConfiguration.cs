using Scalar.AspNetCore;

namespace LeadLeague
{
    internal static class ScalarConfiguration
    {
        public static WebApplication MapScalarApi(this WebApplication app)
        {
            var token = app.Configuration["OktaToken"];

            app.MapScalarApiReference(options =>
            {
                options
                .WithPreferredScheme("Bearer")
                .WithHttpBearerAuthentication(bearer =>
                {
                    bearer.Token = token;
                });
            });

            return app;
        }
    }
}
