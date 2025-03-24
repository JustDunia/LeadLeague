using FastEndpoints;

namespace LeadLeague.Features.Users.GetUser
{
    public class GetUserEndpoint : Endpoint<GetUserRequest, GetUserResponse>
    {
        public override void Configure()
        {
            Get("/{Id}");
            AllowAnonymous();
            Group<UsersGroup>();
            Description(x => x.ClearDefaultAccepts());
        }

        public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
        {
            await SendAsync(new($"Hello {req.Id}"));
        }
    }
}
