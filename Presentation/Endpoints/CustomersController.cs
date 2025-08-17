
using WebApi.Application.Customers.Commands;
using WebApi.Application.Customers.Queries;
using WebApi.Application.Customers.Responses;
using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Presentation.Controllers.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    /// <summary>
    /// Login de cliente. Devuelve JWT si es exitoso.
    /// </summary>
    /// <param name="dto">Credenciales de login.</param>
    /// <returns>Un objeto con el token JWT y el ID del cliente.</returns>
    /// <response code="200">Login exitoso. Devuelve el token y el ID del cliente.</response>
    /// <response code="401">Credenciales inválidas.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginCustomerResponse), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<LoginCustomerResponse>> Login([FromBody] LoginCustomerDto dto)
    {
        var result = await _mediator.Send(new LoginCustomerCommand(dto));
        if (result == null) return Unauthorized();
        return Ok(new LoginCustomerResponse
        {
            Token = result.Token,
            CustomerId = result.CustomerId
        });
    }
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly IMediator _mediator;

    public CustomersController(ICustomerWriteRepository customerWriteRepository, IMediator mediator)
    {
        _customerWriteRepository = customerWriteRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Registra un nuevo cliente con contraseña.
    /// </summary>
    /// <param name="dto">Datos del cliente que vamos a registrar.</param>
    /// <returns>El cliente creado.</returns>
    /// <response code="201">Cliente creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Customer), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Register([FromBody] RegisterCustomerDto dto)
    {
        var result = await _mediator.Send(new RegisterCustomerCommand(dto));
        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtiene todos los clientes.
    /// </summary>
    /// <returns>Lista de clientes.</returns>
    /// <response code="200">Lista de clientes encontrada.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
    {
        var customers = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(customers);
    }

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <returns>El cliente encontrado o NotFound si no existe.</returns>
    /// <response code="200">Cliente encontrado.</response>
    /// <response code="404">Cliente no encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Customer), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Customer>> GetCustomerById(string id)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
        if (customer == null) return NotFound();
        return Ok(customer);
    }
}
