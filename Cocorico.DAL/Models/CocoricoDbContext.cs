using Cocorico.DAL.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.DAL.Models
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

            builder.Entity<SandwichOrder>()
                .HasKey(so => new { so.SandwichId, so.OrderId });
            builder.Entity<SandwichOrder>()
                .HasOne(so => so.Sandwich)
                .WithMany(s => s.SandwichOrders)
                .HasForeignKey(so => so.SandwichId);
            builder.Entity<SandwichOrder>()
                .HasOne(so => so.Order)
                .WithMany(o => o.SandwichOrders)
                .HasForeignKey(so => so.OrderId);
            builder.Entity<SandwichOrder>()
                .HasQueryFilter(so => !so.IsDeleted);

            builder.Entity<SandwichIngredient>()
                .HasKey(si => new { si.IngredientId, si.SandwichId });
            builder.Entity<SandwichIngredient>()
                .HasOne(si => si.Sandwich)
                .WithMany(s => s.SandwichIngredients)
                .HasForeignKey(si => si.SandwichId);
            builder.Entity<SandwichIngredient>()
                .HasOne(si => si.Ingredient)
                .WithMany(i => i.SandwichIngredients)
                .HasForeignKey(si => si.IngredientId);
            builder.Entity<SandwichIngredient>()
                .HasQueryFilter(si => !si.IsDeleted);

            builder.Entity<Ingredient>()
                .HasKey(i => i.Id);
            builder.Entity<Ingredient>()
                .HasMany(i => i.SandwichIngredients)
                .WithOne(si => si.Ingredient)
                .HasForeignKey(si => si.IngredientId);
            builder.Entity<Ingredient>()
                .HasQueryFilter(i => !i.IsDeleted);

            builder.Entity<CocoricoUser>()
                .HasKey(cu => cu.Id);
            builder.Entity<CocoricoUser>()
                .HasMany(cu => cu.Orders)
                .WithOne(o => o.CocoricoUser)
                .HasForeignKey(o => o.CocoricoUserId);
            builder.Entity<CocoricoUser>()
                .HasQueryFilter(cu => !cu.IsDeleted);

            builder.Entity<Sandwich>()
                .HasKey(s => s.Id);
            builder.Entity<Sandwich>()
                .HasMany(s => s.SandwichIngredients)
                .WithOne(si => si.Sandwich)
                .HasForeignKey(si => si.SandwichId);
            builder.Entity<Sandwich>()
                .HasMany(s => s.SandwichOrders)
                .WithOne(uso => uso.Sandwich)
                .HasForeignKey(uso => uso.SandwichId);
            builder.Entity<Sandwich>()
                .HasQueryFilter(s => !s.IsDeleted);

            builder.Entity<Order>()
                .HasKey(o => o.Id);
            builder.Entity<Order>()
                .HasOne(o => o.CocoricoUser)
                .WithMany(cu => cu.Orders)
                .HasForeignKey(o => o.CocoricoUserId);
            builder.Entity<Order>()
                .HasMany(o => o.SandwichOrders)
                .WithOne(so => so.Order)
                .HasForeignKey(so => so.Order);
            builder.Entity<Order>()
                .HasQueryFilter(o => !o.IsDeleted);
            builder.Entity<Order>()
                .Property(o => o.State)
                .HasConversion<int>();
        }
    }
}
