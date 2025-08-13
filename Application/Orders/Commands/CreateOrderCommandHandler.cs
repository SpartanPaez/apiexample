using WebApi.Domain.Entities.Orders;

namespace WebApi.Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IOrderWriteRepository _orderWriteRepository;

    public CreateOrderCommandHandler(IOrderWriteRepository orderWriteRepository)
    {
        _orderWriteRepository = orderWriteRepository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderWriteRepository.AddOrderAsync(request.Order);
        return request.Order;
    }
}
