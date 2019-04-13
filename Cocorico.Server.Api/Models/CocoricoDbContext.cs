using Cocorico.Server.Api.Models.Entities.Sandwich;
using Cocorico.Server.Api.Models.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Server.Api.Models
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<CocoricoUser>()
                .HasQueryFilter(u => !u.IsDeleted)
                .HasIndex(u => new { u.Id, u.Name });

            builder
                .Entity<Sandwich>()
                .HasQueryFilter(s => !s.IsDeleted)
                .HasIndex(s => new { s.Id, s.Name });
        }
    }
}
