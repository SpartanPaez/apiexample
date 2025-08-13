namespace WebApi.Domain.Repositories.Read;

using Domain.Entities.Customers;
public interface ICustomerReadRepository
{
    Task<Customer?> GetByIdAsync(string id);
    Task<IEnumerable<Customer>> GetAllAsync();
}