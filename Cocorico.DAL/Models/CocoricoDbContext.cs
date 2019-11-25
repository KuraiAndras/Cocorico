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

            builder.Entity<UserSandwichOrder>()
                .HasKey(uso => new { uso.UserId, uso.SandwichId, uso.OrderId });
            builder.Entity<UserSandwichOrder>()
                .HasOne(uso => uso.User)
                .WithMany(u => u.UserSandwichOrders)
                .HasForeignKey(uso => uso.UserId);
            builder.Entity<UserSandwichOrder>()
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
                .HasQueryFilter(i => !i.IsDeleted);

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
