using Anonym.DataAccess.Concrete.EntityFramework.Mappings;
using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Concrete.EntityFramework.Contexts
{
    public class AnonymContext : DbContext
    {
        public AnonymContext(DbContextOptions<AnonymContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ChatMessageMap());
            modelBuilder.ApplyConfiguration(new ChatRoomMap());
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
