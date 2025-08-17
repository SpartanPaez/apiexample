using MediatR;
using WebApi.Domain.Entities.Customers;
using WebApi.Domain.Repositories.Write;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Application.Customers.Commands;

public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Customer>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;

    public RegisterCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository)
    {
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<Customer> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        string passwordHash = ComputeSha256Hash(dto.Password!);

        var customer = new Customer
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordHash = passwordHash
        };

        await _customerWriteRepository.AddAsync(customer);
        return customer;
    }

    private static string ComputeSha256Hash(string rawData)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
