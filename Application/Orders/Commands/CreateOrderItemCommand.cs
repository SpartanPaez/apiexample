using WebApi.Domain.Entities.Orders;

namespace WebApi.Application.Orders.Commands;

public record CreateOrderItemCommand(OrderItem Item) : IRequest<OrderItem>;
