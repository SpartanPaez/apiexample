using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Application.Orders.Commands;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItem>
{
    private readonly IOrderItemWriteRepository _orderItemWriteRepository;

    public CreateOrderItemCommandHandler(IOrderItemWriteRepository orderItemWriteRepository)
    {
        _orderItemWriteRepository = orderItemWriteRepository;
    }

    public async Task<OrderItem> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        await _orderItemWriteRepository.AddOrderItemAsync(request.Item);
        return request.Item;
    }
}
