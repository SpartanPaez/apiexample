using WebApi.Domain.Entities.Orders;

namespace WebApi.Domain.Repositories.Write;

public interface IOrderItemWriteRepository
{
    Task AddOrderItemAsync(OrderItem orderItem);
    Task UpdateOrderItemAsync(OrderItem orderItem);
    Task DeleteOrderItemAsync(OrderItem orderItem);
}