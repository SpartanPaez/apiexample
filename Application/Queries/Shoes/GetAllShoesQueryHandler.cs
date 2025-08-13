
using MediatR;
using WebApi.Application.Common.Models;

namespace WebApi.Application.Queries.Shoes;

public class GetAllShoesQueryHandler : IRequestHandler<GetAllShoesQuery, IEnumerable<ShoeDto>>
{
    private readonly IShoeReadRepository _shoeReadRepository;

    public GetAllShoesQueryHandler(IShoeReadRepository shoeReadRepository)
    {
        _shoeReadRepository = shoeReadRepository;
    }

    public async Task<IEnumerable<ShoeDto>> Handle(GetAllShoesQuery request, CancellationToken cancellationToken)
    {
        var shoes = await _shoeReadRepository.GetAllAsync();
        return shoes.ToList();

    }
}