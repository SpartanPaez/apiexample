using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Application.Customers.Queries;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetAllCustomersQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerReadRepository.GetAllAsync();
    }
}
