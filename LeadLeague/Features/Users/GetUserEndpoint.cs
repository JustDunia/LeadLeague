using FastEndpoints;
using LeadLeague.Common.Vms;
using LeadLeague.Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LeadLeague.Features.Users;

public class GetUserEndpoint : Endpoint<GetUserRequest, Results<Ok<GetUserResponse>, NotFound>>
{
    public required AppDbContext DbContext { get; init; }

    public override void Configure()
    {
        Get("/{Id}");
        AllowAnonymous();
        Group<UsersGroup>();
        Description(x => x.ClearDefaultAccepts());
    }

    public override async Task<Results<Ok<GetUserResponse>, NotFound>> ExecuteAsync(GetUserRequest req, CancellationToken ct)
    {
        var user = await DbContext.Users
            .Where(x => x.Id.Equals(req.Id))
            .Select(x => new GetUserResponse(
                x.FirstName,
                x.LastName,
                x.UserName,
                x.Email,
                x.DateOfBirth))
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(user);
    }
}
public sealed record GetUserRequest(Guid Id) : IdVm(Id);

public sealed class GetUserRequestValidator : IdVmValidator<GetUserRequest>;

public sealed record GetUserResponse(
   string FirstName,
   string LastName,
   string UserName,
   string Email,
   DateOnly? DateOfBirth);
