namespace WebApi.Application.Customers.Responses;

/// <summary>
/// Respuesta del login de cliente. Ejemplo unicamente
/// </summary>
public class LoginCustomerResponse
{
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
    public string Token { get; set; } = string.Empty;
    /// <example>123</example>
    public string CustomerId { get; set; } = string.Empty;
}
