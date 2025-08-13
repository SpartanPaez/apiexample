using WebApi.Domain.Entities.Products;

namespace WebApi.Application.Products.Commands;

public class CreateShoeCommandHandler : IRequestHandler<CreateShoeCommand, Shoe>
{
    private readonly IShoeWriteRepository _shoeWriteRepository;

    public CreateShoeCommandHandler(IShoeWriteRepository shoeWriteRepository)
    {
        _shoeWriteRepository = shoeWriteRepository;
    }

    public async Task<Shoe> Handle(CreateShoeCommand request, CancellationToken cancellationToken)
    {
        await _shoeWriteRepository.AddShoeAsync(request.Shoe);
        return request.Shoe;
    }
}
