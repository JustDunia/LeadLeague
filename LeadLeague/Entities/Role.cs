using LeadLeague.Auth;
using LeadLeague.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadLeague.Entities
{
    public sealed class Role : Entity
    {
        public RoleType Name { get; set; }
        public List<User> Users { get; set; } = [];
    }

    public sealed class RoleDbConfiguration : EntityDbConfiguration<Role>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(r => r.Name).IsUnique();

            builder.HasData(
                new
                {
                    Id = new Guid("9029553d-0ac5-42df-879f-970f8a07ba3a"),
                    Name = RoleType.Admin,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = new Guid("c1e2bee5-158d-461d-a964-443a6c119206"),
                    Name = RoleType.Manager,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = new Guid("0948dd81-8f69-4682-9c4c-37e0b16f7262"),
                    Name = RoleType.Office,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = new Guid("b2807e5e-6053-44f3-9869-c6a62741d22b"),
                    Name = RoleType.Coach,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = new Guid("0f75926e-2bd6-472e-983d-89bd6350c359"),
                    Name = RoleType.Athlete,
                    CreatedAt = new DateTime(2025, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
