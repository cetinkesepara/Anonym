using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ChatRoomMap : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable(@"ChatRooms", "dbo");
            builder.HasKey(p => p.ChatRoomId);

            builder.HasOne(cr => cr.Post)
                .WithMany(p => p.ChatRooms)
                .HasForeignKey(cr => cr.PostId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.ChatRoomId).HasColumnName("ChatRoomId");
            builder.Property(p => p.ReviewerUserId).HasColumnName("ReviewerUserId");
            builder.Property(p => p.PostId).HasColumnName("PostId");
            builder.Property(p => p.PublisherName).HasColumnName("PublisherName");
            builder.Property(p => p.ReviewerName).HasColumnName("ReviewerName");
            builder.Property(p => p.PublisherCommented).HasColumnName("PublisherCommented");
            builder.Property(p => p.Active).HasColumnName("Active");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
        }
    }
}
