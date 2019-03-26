using Cocorico.RazorComponents.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cocorico.RazorComponents.Test.Helpers
{
    [TestClass]
    public class ServiceTestBase
    {
        protected SqliteConnection Connection;
        protected DbContextOptions<CocoricoDbContext> Options;

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
    }
}
