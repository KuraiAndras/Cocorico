using Cocorico.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class OpeningConfiguration : IEntityTypeConfiguration<Opening>
    {
        public void Configure(EntityTypeBuilder<Opening> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasMany(o => o.Orders)
                .WithOne(or => or.Opening)
                .HasForeignKey(or => or.OpeningId);
        }
    }
}