using LeadLeague.Common.Vms;

namespace LeadLeague.Features.Users.GetUser
{
    public record GetUserRequest(string Id) : IdVm(Id);
}
