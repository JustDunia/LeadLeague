using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadLeague.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public abstract class EntityDbConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}