using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Server.Domain.Test.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Test.Services
{
    [TestClass]
    public class SandwichServiceTest : ServiceTestBase
    {
        [TestMethod]
        public async Task Add()
        {
            var ingredients = SeedIngredients().ToList();
            var sandwichDto = new SandwichAddDto
            {
                Name = "Test Sandwich",
                Ingredients = ingredients.MapTo<Ingredient, IngredientDto>(),
                Price = 200,
            };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichDto);
            }

            using (var context = NewDbContext)
            {
                var actual = await context
                    .Sandwiches
                    .Include(s => s.Ingredients)
                    .SingleAsync();

                var expected = sandwichDto.MapTo(s => new Sandwich
                {
                    Id = 1,
                    Ingredients = ingredients,
                });

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Ingredients.Count, actual.Ingredients.Count);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Price, actual.Price);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            var ingredients = SeedIngredients();
            var sandwichAddDto = new SandwichAddDto
            {
                Name = "Initial",
                Ingredients = ingredients.MapTo<Ingredient, IngredientDto>()
            };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichAddDto);
            }

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);

                sandwichAddDto.Name = "Updated";

                await service.UpdateSandwichAsync(sandwichAddDto.MapTo(s => new SandwichDto
                {
                    Id = 1,
                }));
            }

            using (var context = NewDbContext)
            {
                var actual = await new ServerSandwichService(context).GetSandwichResultAsync(1);

                var expected = sandwichAddDto.MapTo(s => new SandwichDto
                {
                    Id = 1,
                    Ingredients = s.Ingredients.ToList(),
                });

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Ingredients.Count, actual.Ingredients.Count);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Price, actual.Price);
            }
        }

        [TestMethod]
        public async Task Get()
        {
            var ingredients = SeedIngredients().ToList();
            var sandwichAddDto = new SandwichAddDto
            {
                Name = "Test Sandwich",
                Ingredients = ingredients.MapTo<Ingredient, IngredientDto>(),
                Price = 200,
            };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichAddDto);
            }

            using (var context = NewDbContext)
            {
                var expected = sandwichAddDto.MapTo(s => new SandwichDto
                {
                    Id = 1,
                    Ingredients = s.Ingredients.ToList(),
                });

                var service = new ServerSandwichService(context);
                var actual = await service.GetSandwichResultAsync(expected.Id);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Ingredients.Count, actual.Ingredients.Count);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Price, actual.Price);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            var ingredients = SeedIngredients();
            var sandwichDto = new SandwichAddDto
            {
                Name = "Test Sandwich",
                Ingredients = ingredients.MapTo<Ingredient, IngredientDto>()
            };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichDto);
            }

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.DeleteSandwichAsync(1);
            }

            using (var context = new CocoricoDbContext(Options))
            {
                Assert.AreEqual(0, await context.Sandwiches.CountAsync());
            }
        }

        [TestMethod]
        public async Task GetAll()
        {
            var ingredients = SeedIngredients().ToList();

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(new SandwichAddDto
                {
                    Name = "Test1",
                    Ingredients = ingredients.MapTo<Ingredient, IngredientDto>()
                });
                await service.AddSandwichAsync(new SandwichAddDto
                {
                    Name = "Test2",
                    Ingredients = ingredients.MapTo<Ingredient, IngredientDto>()
                });
            }

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                var result = await service.GetAllSandwichResultAsync();

                Assert.AreEqual(2, result.Count());
            }
        }
    }
}
