using Cocorico.RazorComponents.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cocorico.RazorComponents.Test.Helpers
{
    [TestClass]
    public class ServiceTestBase
    {
        protected SqliteConnection _connection;
        protected DbContextOptions<CocoricoDbContext> _options;

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
    }
}
