using WebApi.Application.Payments.Commands;
using WebApi.Infrastructure.Payments;

namespace WebApi.Presentation.Controllers.Endpoints;

[ApiController]
[Route("api/payments/adamspay")]
public class PaymentsController : ControllerBase
{
    private readonly AdamspayService _adamspayService;

    public PaymentsController(AdamspayService adamspayService)
    {
        _adamspayService = adamspayService;
    }

    /// <summary>
    /// Genera un link de pago Adamspay para una orden.
    /// </summary>
    /// <param name="dto">Datos para crear el link de pago.</param>
    /// <returns>Objeto con la URL del link de pago.</returns>
    /// <response code="200">Link generado exitosamente.</response>
    /// <response code="400">Datos inválidos o error al generar el link.</response>
    [HttpPost("link")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<object>> CreateLink([FromBody] CreateAdamspayLinkDto dto)
    {
        var link = await _adamspayService.CreatePaymentLink(
            dto.Reference,
            dto.Amount.Value,
            dto.Amount.Currency,
            dto.Description,
            dto.CallbackUrl,
            dto.ValidPeriod?.Start,
            dto.ValidPeriod?.End
        );
        if (link == null) return BadRequest("No se pudo generar el link de pago :(");
        return Ok(new { url = link });
    }
}
