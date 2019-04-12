using System.Collections.Generic;
using Cocorico.Server.Models;
using Cocorico.Server.Services.Sandwich;
using Cocorico.Server.Test.Helpers;
using Cocorico.Shared.Dtos.Sandwich;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Helpers;
using Cocorico.Server.Models.Entities.Sandwich;

namespace Cocorico.Server.Test.Services
{
    [TestClass]
    public class SandwichServiceTest : ServiceTestBase
    {
        [TestMethod]
        public async Task Add()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            var expected = sandwichDto.MapTo<NewSandwichDto, Sandwich>();

            expected.Id = 1;

            using (var context = new CocoricoDbContext(Options))
            {
                var actual = await context.Sandwiches.SingleAsync();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            var newSandwichDto = new NewSandwichDto { Name = "Initial" };

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(newSandwichDto);

                newSandwichDto.Id = 1;
                newSandwichDto.Name = "Updated";

                await service.AddOrUpdateAsync(newSandwichDto);
            }

            var expected = newSandwichDto.MapTo<NewSandwichDto, Sandwich>();

            using (var context = new CocoricoDbContext(Options))
            {
                var actual = await context.Sandwiches.SingleAsync();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Get()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            using (var context = new CocoricoDbContext(Options))
            {
                var expected = new SandwichResultDto
                {
                    Id = 1,
                    Name = sandwichDto.Name,
                };

                var service = new SandwichService(context);
                var actual = await service.GetAsync(expected.Id);

                if (actual is Success<SandwichResultDto> result)
                {
                    Assert.AreEqual(expected, result.Data);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.DeleteAsync(1);
            }

            using (var context = new CocoricoDbContext(Options))
            {
                Assert.AreEqual(0, await context.Sandwiches.CountAsync());
            }
        }

        [TestMethod]
        public async Task GetAll()
        {
            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(new NewSandwichDto { Name = "Test1" });
                await service.AddOrUpdateAsync(new NewSandwichDto { Name = "Test2" });
            }

            using (var context = new CocoricoDbContext(Options))
            {
                var service = new SandwichService(context);
                var result = await service.GetAllAsync();

                if (result is Success<IEnumerable<SandwichResultDto>> success)
                {
                    Assert.AreEqual(2, success.Data.Count());
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestCleanup]
        public void Cleanup() => Connection.Close();
    }
}
