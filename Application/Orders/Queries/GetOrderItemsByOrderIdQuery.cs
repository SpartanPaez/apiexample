using MediatR;
using WebApi.Domain.Entities.Orders;

namespace WebApi.Application.Orders.Queries;

public record GetOrderItemsByOrderIdQuery(Guid OrderId) : IRequest<IEnumerable<OrderItem>>;
