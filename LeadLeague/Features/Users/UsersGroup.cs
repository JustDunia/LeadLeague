using FastEndpoints;

namespace LeadLeague.Features.Users
{
    public class UsersGroup : Group
    {
        public UsersGroup()
        {
            Configure("users", ep =>
            {
                ep.Description(x => x
                    .WithTags("Users"));
            });
        }
    }
}
