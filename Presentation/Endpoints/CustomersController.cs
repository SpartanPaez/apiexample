
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Write;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Presentation.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    /// <summary>
    /// Login de cliente. Devuelve JWT si es exitoso.
    /// </summary>
    /// <param name="dto">Credenciales de login.</param>
    /// <returns>JWT o Unauthorized.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromBody] WebApi.Application.Customers.Commands.LoginCustomerDto dto)
    {
        var result = await _mediator.Send(new WebApi.Application.Customers.Commands.LoginCustomerCommand(dto));
        if (result == null) return Unauthorized();
        return Ok(new { token = result.Token, customerId = result.CustomerId });
    }
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly IMediator _mediator;

    public CustomersController(ICustomerWriteRepository customerWriteRepository, IMediator mediator)
    {
        _customerWriteRepository = customerWriteRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Registra un nuevo cliente (con password seguro).
    /// </summary>
    /// <param name="dto">Datos del cliente a registrar.</param>
    /// <returns>El cliente creado.</returns>
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] WebApi.Application.Customers.Commands.RegisterCustomerDto dto)
    {
        var result = await _mediator.Send(new WebApi.Application.Customers.Commands.RegisterCustomerCommand(dto));
        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtiene todos los clientes.
    /// </summary>
    /// <returns>Lista de clientes.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _mediator.Send(new WebApi.Application.Customers.Queries.GetAllCustomersQuery());
        return Ok(customers);
    }

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <returns>El cliente encontrado o NotFound si no existe.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(string id)
    {
        var customer = await _mediator.Send(new WebApi.Application.Customers.Queries.GetCustomerByIdQuery(id));
        if (customer == null) return NotFound();
        return Ok(customer);
    }
}
