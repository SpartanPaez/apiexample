namespace WebApi.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
        builder.Property(c => c.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).HasColumnName("Email").IsRequired().HasMaxLength(200);
        builder.Property(c => c.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(15);
        builder.Property(c => c.PasswordHash).HasColumnName("PasswordHash").IsRequired().HasMaxLength(256);
    }
}