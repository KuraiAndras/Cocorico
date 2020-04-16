using Cocorico.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class SandwichIngredientConfiguration : IEntityTypeConfiguration<SandwichIngredient>
    {
        public void Configure(EntityTypeBuilder<SandwichIngredient> builder)
        {
            builder.HasKey(si => new { si.IngredientId, si.SandwichId });

            builder.HasOne(si => si.Sandwich)
                .WithMany(s => s.SandwichIngredients)
                .HasForeignKey(si => si.SandwichId);

            builder.HasOne(si => si.Ingredient)
                .WithMany(i => i.SandwichIngredients)
                .HasForeignKey(si => si.IngredientId);
        }
    }
}