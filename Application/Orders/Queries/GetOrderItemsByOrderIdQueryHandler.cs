using MediatR;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Application.Orders.Queries;

public class GetOrderItemsByOrderIdQueryHandler : IRequestHandler<GetOrderItemsByOrderIdQuery, IEnumerable<OrderItem>>
{
    private readonly IOrderItemReadRepository _orderItemReadRepository;

    public GetOrderItemsByOrderIdQueryHandler(IOrderItemReadRepository orderItemReadRepository)
    {
        _orderItemReadRepository = orderItemReadRepository;
    }

    public async Task<IEnumerable<OrderItem>> Handle(GetOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderItemReadRepository.GetOrderItemsByOrderIdAsync(request.OrderId);
    }
}
