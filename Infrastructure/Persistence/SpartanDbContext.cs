namespace WebApi.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Entities.Products;

public class SpartanDbContext : DbContext
{
    public SpartanDbContext(DbContextOptions<SpartanDbContext> options) : base(options)
    {

    }
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Shoe> Shoes { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

}