using Cocorico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.SandwichIngredients)
                .WithOne(si => si.Ingredient)
                .HasForeignKey(si => si.IngredientId);
        }
    }
}