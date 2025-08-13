using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities.Orders;

namespace WebApi.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(oi => oi.OrderId).HasColumnName("OrderId").IsRequired().HasMaxLength(50);
        builder.Property(oi => oi.ShoeId).HasColumnName("ShoeId").IsRequired().HasMaxLength(50);
        builder.Property(oi => oi.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(oi => oi.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasColumnType("decimal(18,2)");
    }
}
