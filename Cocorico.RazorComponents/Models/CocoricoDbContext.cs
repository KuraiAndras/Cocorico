using Cocorico.RazorComponents.Models.Entities.Sandwich;
using Cocorico.RazorComponents.Models.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.RazorComponents.Models
{
    public class CocoricoDbContext : IdentityDbContext<CocoricoUser>
    {
        public CocoricoDbContext(DbContextOptions<CocoricoDbContext> options) : base(options)
        {
        }

        public DbSet<Sandwich> Sandwiches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }
}
