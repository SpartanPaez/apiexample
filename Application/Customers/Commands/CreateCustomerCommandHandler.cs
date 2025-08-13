using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Write;

namespace WebApi.Application.Customers.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;

    public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository)
    {
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _customerWriteRepository.AddAsync(request.Customer);
        return request.Customer;
    }
}
