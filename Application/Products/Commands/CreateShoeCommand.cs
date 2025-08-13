using WebApi.Domain.Entities.Products;

namespace WebApi.Application.Products.Commands;

public record CreateShoeCommand(Shoe Shoe) : IRequest<Shoe>;
