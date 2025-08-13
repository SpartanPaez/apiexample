using WebApi.Domain.Entities.Orders;
namespace WebApi.Domain.Repositories.Read;

public interface IOrderReadRepository
{
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
}