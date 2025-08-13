using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Infrastructure.Persistence.Read;

public class OrderItemReadRepository : IOrderItemReadRepository
{
    private readonly SpartanDbContext _context;

    public OrderItemReadRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
    {
        return await _context.OrderItems.AsNoTracking().ToListAsync();
    }

    public async Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId)
    {
        return await _context.OrderItems.FindAsync(orderItemId);
    }
    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .AsNoTracking()
            .ToListAsync();
    }

}