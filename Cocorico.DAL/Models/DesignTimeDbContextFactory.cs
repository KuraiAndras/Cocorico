using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cocorico.DAL.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CocoricoDbContext>
    {
        public CocoricoDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Cocorico.Server.Restful/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<CocoricoDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new CocoricoDbContext(builder.Options);
        }
    }
}