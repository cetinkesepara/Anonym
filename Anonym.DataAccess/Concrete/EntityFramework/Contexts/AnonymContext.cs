using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.EntityFramework.Contexts
{
    public class AnonymContext:DbContext
    {
        public AnonymContext()
        {

        }

        public AnonymContext(DbContextOptions<AnonymContext> options):base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
