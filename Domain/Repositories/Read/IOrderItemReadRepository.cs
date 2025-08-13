using WebApi.Domain.Entities.Orders;

namespace WebApi.Domain.Repositories.Read;

public interface IOrderItemReadRepository
{
    Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);
    Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
}