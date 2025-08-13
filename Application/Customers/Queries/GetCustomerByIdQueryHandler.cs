using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Read;

namespace WebApi.Application.Customers.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetCustomerByIdQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _customerReadRepository.GetByIdAsync(request.Id);
    }
}
