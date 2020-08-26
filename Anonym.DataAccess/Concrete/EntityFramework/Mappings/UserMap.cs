using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users", "dbo");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            builder.HasMany(u => u.UserClaims).WithOne(uc => uc.User).HasForeignKey(uc => uc.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.UserLogins).WithOne(ul => ul.User).HasForeignKey(ul => ul.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
