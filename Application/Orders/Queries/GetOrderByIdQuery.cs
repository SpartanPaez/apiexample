using MediatR;
using WebApi.Domain.Entities.Orders;

namespace WebApi.Application.Orders.Queries;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order>;
