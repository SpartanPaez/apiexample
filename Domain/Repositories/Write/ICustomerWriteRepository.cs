namespace WebApi.Domain.Repositories.Write;
using Domain.Entities.Customers;

public interface ICustomerWriteRepository
{
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}
