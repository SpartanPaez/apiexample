using WebApi.Domain.Entities.Orders;

namespace WebApi.Application.Orders.Commands;

public record CreateOrderCommand(Order Order) : IRequest<Order>;
