using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(@"Categories", "dbo");
            builder.HasKey(p => p.CategoryId);

            builder.Property(p => p.CategoryId).HasColumnName("CategoryId");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Description).HasColumnName("Description");
            builder.Property(p => p.Active).HasColumnName("Active");
        }
    }
}
