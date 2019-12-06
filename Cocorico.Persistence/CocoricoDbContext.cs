using Cocorico.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Persistence
{
    public sealed class CocoricoDbContext : IdentityDbContext<CocoricoUser>
    {
        public CocoricoDbContext(DbContextOptions<CocoricoDbContext> options) : base(options)
        {
        }

        public DbSet<Sandwich> Sandwiches { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Opening> Openings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(typeof(CocoricoDbContext).Assembly);
    }
}
