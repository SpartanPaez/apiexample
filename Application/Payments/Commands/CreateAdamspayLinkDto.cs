namespace WebApi.Application.Payments.Commands;

public class CreateAdamspayLinkDto
{
    public string Reference { get; set; } = null!; // docId
    public AdamspayAmountDto Amount { get; set; } = null!;
    public string? Description { get; set; } // label
    public string? CallbackUrl { get; set; }
    public AdamspayValidPeriodDto? ValidPeriod { get; set; }
}

public class AdamspayAmountDto
{
    public string Currency { get; set; } = "PYG";
    public decimal Value { get; set; }
}

public class AdamspayValidPeriodDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
