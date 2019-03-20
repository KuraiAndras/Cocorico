using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Model;
using Cocorico.Server.Model.Entities.Sandwich;
using Cocorico.Server.Services.Sandwich;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cocorico.Server.Test.Services
{
    [TestClass]
    public class SandwichServiceTest
    {
        private SqliteConnection _connection;
        private DbContextOptions<CocoricoDbContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<CocoricoDbContext>()
                .UseSqlite(_connection)
                .Options;

            using (var context = new CocoricoDbContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestMethod]
        public async Task Add()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            var expected = sandwichDto.ToSandwich();
            expected.Id = 1;

            using (var context = new CocoricoDbContext(_options))
            {
                var actual = await context.Sandwiches.SingleAsync();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Update()
        {
            var newSandwichDto = new NewSandwichDto { Name = "Initial" };

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(newSandwichDto);

                newSandwichDto.Id = 1;
                newSandwichDto.Name = "Updated";

                await service.AddOrUpdateAsync(newSandwichDto);
            }

            var expected = newSandwichDto.ToSandwich();

            using (var context = new CocoricoDbContext(_options))
            {
                var actual = await context.Sandwiches.SingleAsync();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Get()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            using (var context = new CocoricoDbContext(_options))
            {
                var expected = new SandwichResultDto
                {
                    Id = 1,
                    Name = sandwichDto.Name,
                };

                var service = new SandwichService(context);
                var actual = await service.GetAsync(expected.Id);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            var sandwichDto = new NewSandwichDto { Name = "Test Sandwich" };

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(sandwichDto);
            }

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.DeleteAsync(1);
            }

            using (var context = new CocoricoDbContext(_options))
            {
                Assert.AreEqual(0, await context.Sandwiches.CountAsync());
            }
        }

        [TestMethod]
        public async Task GetAll()
        {
            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                await service.AddOrUpdateAsync(new NewSandwichDto { Name = "Test1" });
                await service.AddOrUpdateAsync(new NewSandwichDto { Name = "Test2" });
            }

            using (var context = new CocoricoDbContext(_options))
            {
                var service = new SandwichService(context);
                var result = await service.GetAllAsync();

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestCleanup]
        public void Cleanup() => _connection.Close();
    }
}
