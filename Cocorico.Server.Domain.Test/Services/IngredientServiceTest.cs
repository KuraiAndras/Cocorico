using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Ingredient;
using Cocorico.Server.Domain.Test.Helpers;
using Cocorico.Shared.Dtos.Ingredient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cocorico.Server.Domain.Test.Services
{
    [TestClass]
    public class IngredientServiceTest : ServiceTestBase
    {
        [TestMethod]
        public async Task AddIngredient()
        {
            var addDto = await CreateIngredient();

            using (var context = NewDbContext)
            {
                var actual = await context.Ingredients.SingleOrDefaultAsync(i => i.Id == 1);

                Assert.AreEqual(addDto.MapTo(i => new Ingredient { Id = 1, IsDeleted = false }), actual);
            }
        }

        [TestMethod]
        public async Task UpdateIngredient()
        {
            await CreateIngredient();

            var dto = new IngredientDto
            {
                Id = 1,
                Name = "Updated"
            };

            using (var context = NewDbContext)
            {
                var service = new ServerIngredientService(context);

                await service.UpdateAsync(dto);
            }

            using (var context = NewDbContext)
            {
                var actual = await context.Ingredients.SingleOrDefaultAsync(i => i.Id == dto.Id);

                var expected = dto.MapTo<IngredientDto, Ingredient>();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task GetIngredient()
        {
            var dto = await CreateIngredient();

            using (var context = NewDbContext)
            {
                var service = new ServerIngredientService(context);

                var actual = await service.GetAsync(1);
                var expected = dto.MapTo(i => new IngredientDto { Id = 1 });

                Assert.AreEqual(expected, actual);
            }
        }

        private async Task<IngredientAddDto> CreateIngredient()
        {
            var addDto = new IngredientAddDto { Name = "Test" };

            using (var context = NewDbContext)
            {
                var service = new ServerIngredientService(context);

                await service.AddAsync(addDto);
            }

            return addDto;
        }
    }
}
