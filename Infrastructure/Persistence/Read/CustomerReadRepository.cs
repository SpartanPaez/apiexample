using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Infrastructure.Persistence.Read;
public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly SpartanDbContext _context;

    public CustomerReadRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _context.Customers.FindAsync(id);  
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }
}