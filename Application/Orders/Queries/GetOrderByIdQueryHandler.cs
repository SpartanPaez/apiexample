using MediatR;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Application.Orders.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderReadRepository.GetOrderByIdAsync(request.OrderId);
    }
}
