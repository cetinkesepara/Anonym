using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(@"Posts", "dbo");
            builder.HasKey(p => p.PostId);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.PostId).HasColumnName("PostId");
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.Property(p => p.CategoryId).HasColumnName("CategoryId");
            builder.Property(p => p.Title).HasColumnName("Title");
            builder.Property(p => p.Text).HasColumnName("Text");
            builder.Property(p => p.CreatePostDate).HasColumnName("CreatePostDate");
            builder.Property(p => p.IsActive).HasColumnName("IsActive");
        }
    }
}
