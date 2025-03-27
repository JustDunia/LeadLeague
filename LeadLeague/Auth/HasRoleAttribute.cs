using Microsoft.AspNetCore.Authorization;

namespace LeadLeague.Auth;

public class HasRoleAttribute : AuthorizeAttribute
{
    public HasRoleAttribute(params RoleType[] roles)
    {
        var roleNames = roles.Select(x => x.ToString());
        Roles = string.Join(",", roleNames);
    }
}
