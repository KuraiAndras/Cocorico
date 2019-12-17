using Cocorico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cocorico.Persistence.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.CocoricoUser)
                .WithMany(cu => cu.Orders)
                .HasForeignKey(o => o.CocoricoUserId);

            builder.HasMany(o => o.SandwichOrders)
                .WithOne(so => so.Order)
                .HasForeignKey(so => so.OrderId);

            builder.Property(o => o.State)
                .HasConversion<int>();
        }
    }
}