using System.Collections.Generic;
using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Cocorico.Server.Domain.Test.Helpers
{
    [TestClass]
    public class ServiceTestBase
    {
        protected SqliteConnection Connection;
        protected DbContextOptions<CocoricoDbContext> Options;

        protected CocoricoDbContext NewDbContext => new CocoricoDbContext(Options);

        [TestInitialize]
        public void Initialize()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();

            Options = new DbContextOptionsBuilder<CocoricoDbContext>()
                .UseSqlite(Connection)
                .Options;

            using (var context = new CocoricoDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        protected CocoricoUser SeedUsers()
        {
            var user = new CocoricoUser
            {
                Email = "test1@gmail.com",
                EmailConfirmed = true,
                Name = "Test Name",
                UserName = "Test UserName",
            };

            using (var context = NewDbContext)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = NewDbContext)
            {
                user = context.Users.First();
            }

            return user;
        }

        protected List<Sandwich> SeedSandwiches()
        {
            var sandwiches = new List<Sandwich>
            {
                new Sandwich
                {
                    Name = "Test Name1",
                    Price = 40
                },
                new Sandwich
                {
                    Name = "Test Name2",
                    Price = 50
                },
                new Sandwich
                {
                    Name = "Test Name3",
                    Price = 60
                },
                new Sandwich
                {
                    Name = "Test Name4",
                    Price = 70
                },
                new Sandwich
                {
                    Name = "Test Name5",
                    Price = 80
                },
            };

            using (var context = NewDbContext)
            {
                context.AddRange(sandwiches);
                context.SaveChanges();
            }

            return sandwiches;
        }
    }
}
