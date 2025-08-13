using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    [HttpGet]
    public async Task<IActionResult> GetAllShoes()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var shoes = await _mediator.Send(new GetAllShoesQuery());
        stopwatch.Stop();
        Console.WriteLine($"[ShoesController] Tiempo total endpoint GetAllShoes: {stopwatch.ElapsedMilliseconds} ms");
        return Ok(shoes);
    }

    /// <summary>
    /// Crea un nuevo zapato.
    /// </summary>
    /// <param name="shoe">Datos del zapato a crear.</param>
    /// <returns>El zapato creado.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateShoe([FromBody] Shoe shoe)
    {
        var result = await _mediator.Send(new WebApi.Application.Products.Commands.CreateShoeCommand(shoe));
        return CreatedAtAction(nameof(CreateShoe), new { id = result.Id }, result);
    }
}
