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

            builder.Entity<Ingredient>()
                .HasQueryFilter(i => !i.IsDeleted);

            builder.Entity<SandwichIngredient>()
                .HasQueryFilter(si => !si.IsDeleted);

            builder.Entity<UserSandwichOrder>()
                .HasQueryFilter(so => !so.IsDeleted);

            builder.Entity<CocoricoUser>()
                .HasQueryFilter(cu => !cu.IsDeleted);

            builder.Entity<Sandwich>()
                .HasQueryFilter(s => !s.IsDeleted);

            builder.Entity<Order>()
                .HasQueryFilter(o => !o.IsDeleted);

            builder.Entity<Order>()
                .Property(o => o.State)
                .HasConversion<int>();
        }
    }
}
