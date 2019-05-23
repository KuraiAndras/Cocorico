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

        protected IEnumerable<Sandwich> SeedSandwiches()
        {
            SeedIngredients();

            var sandwiches = new List<Sandwich>();
            using (var context = NewDbContext)
            {
                sandwiches.AddRange(new List<Sandwich>
                {
                    new Sandwich
                    {
                        Name = "Test Name1",
                        Price = 40,
                        Ingredients = context.Ingredients.Where(i =>
                                i.Id == 1)
                            .ToList(),
                    },
                    new Sandwich
                    {
                        Name = "Test Name2",
                        Price = 50,
                        Ingredients = context.Ingredients.Where(i =>
                                i.Id == 1
                                || i.Id == 2)
                            .ToList(),
                    },
                    new Sandwich
                    {
                        Name = "Test Name3",
                        Price = 60,
                        Ingredients = context.Ingredients.Where(i =>
                                i.Id == 1
                                || i.Id == 2
                                || i.Id == 3)
                            .ToList(),
                    },
                    new Sandwich
                    {
                        Name = "Test Name4",
                        Price = 70,
                        Ingredients = context.Ingredients.Where(i =>
                                i.Id == 1
                                || i.Id == 2
                                || i.Id == 3
                                || i.Id == 4)
                            .ToList(),
                    },
                    new Sandwich
                    {
                        Name = "Test Name5",
                        Price = 80,
                        Ingredients = context.Ingredients.Where(i =>
                                i.Id == 1
                                || i.Id == 2
                                || i.Id == 3
                                || i.Id == 4
                                || i.Id == 5)
                            .ToList(),
                    },
                });

                context.Sandwiches.AddRange(sandwiches);
                context.SaveChanges();
            }

            return sandwiches;
        }

        protected IEnumerable<Ingredient> SeedIngredients()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient{Name = "Test 1"},
                new Ingredient{Name = "Test 2"},
                new Ingredient{Name = "Test 3"},
                new Ingredient{Name = "Test 4"},
                new Ingredient{Name = "Test 5"},
            };

            using (var context = NewDbContext)
            {
                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();
            }

            return ingredients;
        }

        [TestCleanup]
        public void Cleanup() => Connection.Close();
    }
}
