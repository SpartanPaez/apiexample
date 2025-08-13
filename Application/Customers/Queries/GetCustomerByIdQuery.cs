using WebApi.Domain.Entities.Customers;

namespace WebApi.Application.Customers.Queries;

public record GetCustomerByIdQuery(string Id) : IRequest<Customer?>;
