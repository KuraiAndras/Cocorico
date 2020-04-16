using Cocorico.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class SandwichConfiguration : IEntityTypeConfiguration<Sandwich>
    {
        public void Configure(EntityTypeBuilder<Sandwich> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.SandwichIngredients)
                .WithOne(si => si.Sandwich)
                .HasForeignKey(si => si.SandwichId);

            builder.HasMany(s => s.SandwichOrders)
                .WithOne(uso => uso.Sandwich)
                .HasForeignKey(uso => uso.SandwichId);
        }
    }
}