using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(@"Roles", "dbo");

            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(r => r.Name).HasMaxLength(256);
            builder.Property(r => r.NormalizedName).HasMaxLength(256);

            builder.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.RoleClaims).WithOne(rc => rc.Role).HasForeignKey(rc => rc.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
