using MediatR;

namespace WebApi.Application.Customers.Commands;

public record LoginCustomerCommand(LoginCustomerDto Dto) : IRequest<LoginCustomerResult?>; // Retorna DTO o null

public class LoginCustomerResult
{
    public string Token { get; set; } = null!;
    public string CustomerId { get; set; } = null!;
}
