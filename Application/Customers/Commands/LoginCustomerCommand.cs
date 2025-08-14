using MediatR;

namespace WebApi.Application.Customers.Commands;

public record LoginCustomerCommand(LoginCustomerDto Dto) : IRequest<string?>; // Retorna JWT o null
