using LeadLeague.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadLeague.Entities;

public sealed class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required string UserName { get; set; } = string.Empty;
    public required string Email { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public required string OktaId { get; set; }
    public List<Role> Roles { get; set; } = [];
}

public sealed class UserDbConfiguration : EntityDbConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.OktaId).IsUnique();
    }
}