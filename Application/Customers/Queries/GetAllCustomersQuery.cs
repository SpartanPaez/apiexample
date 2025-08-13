using WebApi.Domain.Entities.Customers;

namespace WebApi.Application.Customers.Queries;

public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;
