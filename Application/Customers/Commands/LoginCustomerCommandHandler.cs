using MediatR;
using WebApi.Domain.Repositories.Read;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Application.Customers.Commands;

public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, string?>
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IConfiguration _configuration;

    public LoginCustomerCommandHandler(ICustomerReadRepository customerReadRepository, IConfiguration configuration)
    {
        _customerReadRepository = customerReadRepository;
        _configuration = configuration;
    }

    public async Task<string?> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var customers = await _customerReadRepository.GetAllAsync();
        var customer = customers.FirstOrDefault(c => c.Email == dto.Email);
        if (customer == null) return null;

        string hash = ComputeSha256Hash(dto.Password!);
        if (customer.PasswordHash != hash) return null;

        // Generar JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id!),
                new Claim(ClaimTypes.Email, customer.Email!)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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
