using Core.DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anonym.DataAccess.Tests.EntityFramework
{
    public static class DbOptionsFactory
    {
        static DbOptionsFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config["ConnectionStrings:SQLProvider"];

            DbContextOptions = new DbContextOptionsBuilder<AnonymContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public static DbContextOptions<AnonymContext> DbContextOptions { get; }
    }
}
