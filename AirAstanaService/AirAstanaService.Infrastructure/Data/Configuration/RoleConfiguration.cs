using AirAstanaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirAstanaService.Infrastructure.Data;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.ID);

        builder.Property(r => r.Code)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(r => r.Code)
            .IsUnique();
    }
}