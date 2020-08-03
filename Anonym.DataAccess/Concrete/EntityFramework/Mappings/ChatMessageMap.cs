using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Mappings
{
    public class ChatMessageMap : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable(@"ChatMessages", "dbo");
            builder.HasKey(p => p.ChatMessageId);

            builder.HasOne(cm => cm.ChatRoom)
                .WithMany(cr => cr.ChatMessages)
                .HasForeignKey(cm => cm.ChatRoomId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.ChatMessageId).HasColumnName("ChatMessageId");
            builder.Property(p => p.ChatRoomId).HasColumnName("ChatRoomId");
            builder.Property(p => p.DisplayUserName).HasColumnName("DisplayUserName");
            builder.Property(p => p.Message).HasColumnName("Message");
            builder.Property(p => p.IsPublisherMessage).HasColumnName("IsPublisherMessage");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
        }
    }
}
