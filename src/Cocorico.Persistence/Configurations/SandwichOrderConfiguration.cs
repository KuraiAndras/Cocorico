using Cocorico.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class SandwichOrderConfiguration : IEntityTypeConfiguration<SandwichOrder>
    {
        public void Configure(EntityTypeBuilder<SandwichOrder> builder)
        {
            builder.HasKey(so => so.Id);

            builder.HasOne(so => so.Sandwich)
                .WithMany(s => s.SandwichOrders)
                .HasForeignKey(so => so.SandwichId);

            builder.HasOne(so => so.Order)
                .WithMany(o => o.SandwichOrders)
                .HasForeignKey(so => so.OrderId);
        }
    }
}
