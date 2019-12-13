using Cocorico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class IngredientModificationConfiguration : IEntityTypeConfiguration<IngredientModification>
    {
        public void Configure(EntityTypeBuilder<IngredientModification> builder)
        {
            builder.HasKey(im => im.Id);

            builder.HasOne(im => im.Ingredient)
                .WithMany(i => i.IngredientModifications)
                .HasForeignKey(im => im.IngredientId);

            builder.HasOne(im => im.SandwichOrder)
                .WithMany(so => so.IngredientModifications)
                .HasForeignKey(im => im.SandwichOrderId);

            builder.Property(im => im.Modification)
                .HasConversion<int>();
        }
    }
}