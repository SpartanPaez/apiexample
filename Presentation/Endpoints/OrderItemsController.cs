using WebApi.Application.Orders.Commands;
using WebApi.Application.Orders.Queries;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Presentation.Controllers.Endpoints;

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
    /// <response code="200">Lista de items encontrada.</response>
    /// <response code="404">No se encontraron items para la orden.</response>
    [HttpGet("byorder/{orderId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderItem>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(Guid orderId)
    {
        var items = await _mediator.Send(new GetOrderItemsByOrderIdQuery(orderId));
        if (items == null || !items.Any()) return NotFound();
        return Ok(items);
    }

    /// <summary>
    /// Crea un nuevo item de orden.
    /// </summary>
    /// <param name="item">Datos del item a crear.</param>
    /// <returns>El item creado.</returns>
    /// <response code="201">Item creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderItem), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateOrderItem([FromBody] OrderItem item)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(item));
        return CreatedAtAction(nameof(GetOrderItemsByOrderId), new { orderId = result.OrderId }, result);
    }
}
