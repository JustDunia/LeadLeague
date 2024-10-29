using LeadLeague.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadLeague.Entities;

public sealed class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required string Email { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}

public sealed class UserDbConfiguration : EntityDbConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
    }
}