using WebApi.Application.Common.Models;
using WebApi.Infrastructure.Persistence;

public class ShoeReadRepository : IShoeReadRepository
{
    private readonly SpartanDbContext _context;

    public ShoeReadRepository(SpartanDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ShoeDto>> GetAllAsync()
    {
        return await _context.Shoes
            .AsNoTracking()
            .Where(s => s.Stock > 0)
            .Select(s => new ShoeDto
            {
                Id = s.Id.ToString(),
                Brand = s.Brand,
                Model = s.Model,
                Price = s.Price,
                Stock = s.Stock,
                Season = s.Season
            })
            .ToListAsync();
    }
}
