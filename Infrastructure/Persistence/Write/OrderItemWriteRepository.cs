using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Infrastructure.Persistence.Write;

public class OrderItemWriteRepository : IOrderItemWriteRepository
{
    private readonly SpartanDbContext _context;

    public OrderItemWriteRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }
}