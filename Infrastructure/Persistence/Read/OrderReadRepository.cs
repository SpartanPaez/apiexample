using WebApi.Domain.Entities.Orders;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Domain.Repositories.Read;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly SpartanDbContext _context;

    public OrderReadRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        return order!;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }
}