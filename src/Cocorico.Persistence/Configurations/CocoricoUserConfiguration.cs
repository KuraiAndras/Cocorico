using Cocorico.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class CocoricoUserConfiguration : IEntityTypeConfiguration<CocoricoUser>
    {
        public void Configure(EntityTypeBuilder<CocoricoUser> builder)
        {
            builder.HasKey(cu => cu.Id);

            builder.HasMany(cu => cu.Orders)
                .WithOne(o => o.CocoricoUser)
                .HasForeignKey(o => o.CocoricoUserId);
        }
    }
}