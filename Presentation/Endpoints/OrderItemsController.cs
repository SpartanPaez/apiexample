using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Orders.Queries;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Presentation.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderItemWriteRepository _orderItemWriteRepository;

    public OrderItemsController(IMediator mediator, IOrderItemWriteRepository orderItemWriteRepository)
    {
        _mediator = mediator;
        _orderItemWriteRepository = orderItemWriteRepository;
    }

    /// <summary>
    /// Obtiene los items de una orden específica.
    /// </summary>
    /// <param name="orderId">Identificador de la orden (GUID).</param>
    /// <returns>Lista de items de la orden.</returns>
    [HttpGet("byorder/{orderId}")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(Guid orderId)
    {
        var items = await _mediator.Send(new GetOrderItemsByOrderIdQuery(orderId));
        return Ok(items);
    }

    /// <summary>
    /// Crea un nuevo item de orden.
    /// </summary>
    /// <param name="item">Datos del item a crear.</param>
    /// <returns>El item creado.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateOrderItem([FromBody] OrderItem item)
    {
        var result = await _mediator.Send(new WebApi.Application.Orders.Commands.CreateOrderItemCommand(item));
        return CreatedAtAction(nameof(GetOrderItemsByOrderId), new { orderId = result.OrderId }, result);
    }
}
