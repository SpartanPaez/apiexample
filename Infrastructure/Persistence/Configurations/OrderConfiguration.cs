using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities.Orders;

namespace WebApi.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("Id").IsRequired().HasMaxLength(50);
        builder.Property(o => o.CustomerId).HasColumnName("CustomerId").IsRequired().HasMaxLength(50);
    }
}