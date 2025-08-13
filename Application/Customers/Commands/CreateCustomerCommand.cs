using WebApi.Domain.Entities.Customers;

namespace WebApi.Application.Customers.Commands;

public record CreateCustomerCommand(Customer Customer) : IRequest<Customer>;
