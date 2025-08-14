using MediatR;
using WebApi.Domain.Entities.Customers;

namespace WebApi.Application.Customers.Commands;

public record RegisterCustomerCommand(RegisterCustomerDto Dto) : IRequest<Customer>;
