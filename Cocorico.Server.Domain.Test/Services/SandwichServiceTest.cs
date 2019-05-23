using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Server.Domain.Test.Helpers;
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
            var sandwichDto = new SandwichAddDto { Name = "Test Sandwich" };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichDto);
            }

            using (var context = NewDbContext)
            {
                var actual = await context.Sandwiches.SingleAsync();

                var expected = sandwichDto.MapTo(s => new Sandwich
                {
                    Id = 1,
                });

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            var newSandwichDto = new SandwichAddDto { Name = "Initial" };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(newSandwichDto);
            }

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);

                newSandwichDto.Name = "Updated";

                await service.UpdateSandwichAsync(newSandwichDto.MapTo(s => new SandwichDto
                {
                    Id = 1,
                }));
            }

            var expected = newSandwichDto.MapTo(s => new Sandwich
            {
                Id = 1,
            });

            using (var context = NewDbContext)
            {
                var actual = await context.Sandwiches.SingleAsync();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Get()
        {
            var sandwichDto = new SandwichAddDto { Name = "Test Sandwich" };

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(sandwichDto);
            }

            using (var context = NewDbContext)
            {
                var expected = new SandwichDto
                {
                    Id = 1,
                    Name = sandwichDto.Name,
                };

                var service = new ServerSandwichService(context);
                var actual = await service.GetSandwichResultAsync(expected.Id);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            var sandwichDto = new SandwichAddDto { Name = "Test Sandwich" };

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
            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                await service.AddSandwichAsync(new SandwichAddDto { Name = "Test1" });
                await service.AddSandwichAsync(new SandwichAddDto { Name = "Test2" });
            }

            using (var context = NewDbContext)
            {
                var service = new ServerSandwichService(context);
                var result = await service.GetAllSandwichResultAsync();

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestCleanup]
        public void Cleanup() => Connection.Close();
    }
}
