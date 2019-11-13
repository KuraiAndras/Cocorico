using Cocorico.Server.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Server.Domain.Models
{
    public class CocoricoDbContext : IdentityDbContext<CocoricoUser>
    {
        public CocoricoDbContext(DbContextOptions<CocoricoDbContext> options) : base(options)
        {
        }

        public DbSet<Sandwich> Sandwiches { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SandwichIngredient>()
                .HasKey(s => new { s.SandwichId, s.IngredientId });

            builder.Entity<SandwichOrder>()
                .HasKey(s => new { s.SandwichId, s.OrderId });

            builder
                .Entity<CocoricoUser>()
                .HasQueryFilter(u => !u.IsDeleted)
                .HasIndex(u => new { u.Id, u.Name });

            builder
                .Entity<Sandwich>()
                .HasQueryFilter(s => !s.IsDeleted)
                .HasIndex(s => new { s.Id, s.Name });

            builder
                .Entity<Order>()
                .HasQueryFilter(o => !o.IsDeleted)
                .HasIndex(o => new { o.Id, o.CustomerId });

            builder
                .Entity<Order>()
                .Property(o => o.State)
                .HasConversion<int>();

            builder
                .Entity<Ingredient>()
                .HasQueryFilter(i => !i.IsDeleted)
                .HasIndex(i => new { i.Id, i.Name });
        }
    }
}
