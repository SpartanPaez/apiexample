using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Payments.Commands;
using WebApi.Infrastructure.Payments;

namespace WebApi.Presentation.Endpoints;

[ApiController]
[Route("api/payments/adamspay")]
public class PaymentsController : ControllerBase
{
    private readonly AdamspayService _adamspayService;

    public PaymentsController(AdamspayService adamspayService)
    {
        _adamspayService = adamspayService;
    }

    [HttpPost("link")]
    public async Task<ActionResult<string>> CreateLink([FromBody] CreateAdamspayLinkDto dto)
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
        if (link == null) return BadRequest("No se pudo generar el link de pago");
        return Ok(new { url = link });
    }
}
