using System.Security.Claims;
using LeadLeague.Database;
using LeadLeague.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadLeague.Auth;

public sealed class UserContextMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, AppDbContext dbContext)
    {
        var user = context.User;

        if (user?.Identity?.IsAuthenticated ?? false)
        {
            var dbUser = await GetOrCreateUser(user, dbContext);
            AddRolesToUserClaims(user, dbUser.Roles);
        }

        await next(context);
    }

    private async Task<User> GetOrCreateUser(ClaimsPrincipal? user, AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(user);

        var oktaId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var dbUser = await dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.OktaId == oktaId);

        if (dbUser is null)
        {
            var userName = user.FindFirstValue("nickname")!;
            var email = user.FindFirstValue("name")!;
            dbUser = new User
            {
                UserName = userName,
                Email = email,
                OktaId = oktaId,
                Roles = [new Role { Name = RoleType.Athlete }]
            };
            dbContext.Add(dbUser);
            await dbContext.SaveChangesAsync();
        }

        return dbUser;
    }

    private void AddRolesToUserClaims(ClaimsPrincipal user, IEnumerable<Role> roles)
    {
        var claims = roles.Select(x => new Claim(ClaimTypes.Role, x.Name.ToString()));
        user.AddIdentity(new(claims));
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
