namespace WebApi.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities.Products;

public class ShoeConfiguration : IEntityTypeConfiguration<Shoe>
{
    public void Configure(EntityTypeBuilder<Shoe> builder)
    {
        builder.ToTable("Shoes");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.Brand).HasColumnName("Brand").IsRequired().HasMaxLength(100);
        builder.Property(s => s.Model).HasColumnName("Model").IsRequired().HasMaxLength(100);
        builder.Property(s => s.Size).HasColumnName("Size").IsRequired().HasMaxLength(10);
        builder.Property(s => s.Price).HasColumnName("Price").IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(s => s.Stock).HasColumnName("Stock").IsRequired();
        builder.Property(s => s.Season).HasColumnName("Season").HasMaxLength(50);
    }
}   