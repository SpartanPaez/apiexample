using WebApi.Domain.Entities.Orders;

public interface IOrderWriteRepository
{
    Task AddOrderAsync(Order order);
}
