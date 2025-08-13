using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Orders.Queries;
using WebApi.Domain.Entities.Orders;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Presentation.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderWriteRepository _orderWriteRepository;

    public OrdersController(IMediator mediator, IOrderWriteRepository orderWriteRepository)
    {
        _mediator = mediator;
        _orderWriteRepository = orderWriteRepository;
    }

    /// <summary>
    /// Obtiene una orden por su identificador.
    /// </summary>
    /// <param name="id">Identificador de la orden (GUID).</param>
    /// <returns>La orden encontrada o NotFound si no existe.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null) return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Crea una nueva orden.
    /// </summary>
    /// <param name="order">Datos de la orden a crear.</param>
    /// <returns>La orden creada.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] Order order)
    {
        var result = await _mediator.Send(new WebApi.Application.Orders.Commands.CreateOrderCommand(order));
        return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
    }

}
