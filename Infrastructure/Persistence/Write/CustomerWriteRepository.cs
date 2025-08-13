using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Infrastructure.Persistence.Write;

public class CustomerWriteRepository : ICustomerWriteRepository
{
    private readonly SpartanDbContext _context;

    public CustomerWriteRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}