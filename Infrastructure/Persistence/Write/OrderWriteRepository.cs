using WebApi.Domain.Entities.Orders;

namespace WebApi.Infrastructure.Persistence.Write;

public class OrderWriteRepository : IOrderWriteRepository
{
    private readonly SpartanDbContext _context;

    public OrderWriteRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }


}