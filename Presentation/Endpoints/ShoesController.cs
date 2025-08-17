using WebApi.Application.Products.Commands;
using WebApi.Application.Queries.Shoes;
using WebApi.Domain.Entities.Products;

namespace WebApi.Presentation.Controllers.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class ShoesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IShoeWriteRepository _shoeWriteRepository;

    public ShoesController(IMediator mediator, IShoeWriteRepository shoeWriteRepository)
    {
        _mediator = mediator;
        _shoeWriteRepository = shoeWriteRepository;
    }

    /// <summary>
    /// Obtiene todos los zapatos disponibles.
    /// </summary>
    /// <returns>Lista de zapatos.</returns>
    /// <response code="200">Lista de zapatos encontrada.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Shoe>), 200)]
    public async Task<IActionResult> GetAllShoes()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var shoes = await _mediator.Send(new GetAllShoesQuery());
        stopwatch.Stop();
        return Ok(shoes);
    }

    /// <summary>
    /// Crea un nuevo zapato.
    /// </summary>
    /// <param name="shoe">Datos del zapato a crear.</param>
    /// <returns>El zapato creado.</returns>
    /// <response code="201">Zapato creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Shoe), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateShoe([FromBody] Shoe shoe)
    {
        var result = await _mediator.Send(new CreateShoeCommand(shoe));
        return CreatedAtAction(nameof(CreateShoe), new { id = result.Id }, result);
    }
}
