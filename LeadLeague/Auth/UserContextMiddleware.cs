using LeadLeague.Database;
using System.Security.Claims;

namespace LeadLeague.Auth
{
    public class UserContextMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context, AppDbContext dbContext)
        {
            var user = context.User;

            if (user?.Identity?.IsAuthenticated ?? false)
            {
                var oktaId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // TODO implement logic to find or create user
                //var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.OktaId == oktaId);
            }

            await next(context);
        }
    }

    public static class UserContextMiddlewareExtenstion
    {
        public static IApplicationBuilder UseUserContext(this IApplicationBuilder app)
        {
            app.UseMiddleware<UserContextMiddleware>();
            return app;
        }
    }
}
