using WebApi.Domain.Entities.Products;
using WebApi.Domain.Repositories.Write;
using WebApi.Infrastructure.Persistence;

public class ShoeWriteRepository : IShoeWriteRepository
{
    private readonly SpartanDbContext _context;

    public ShoeWriteRepository(SpartanDbContext context)
    {
        _context = context;
    }

    public async Task AddShoeAsync(Shoe shoe)
    {
        await _context.Shoes.AddAsync(shoe);
        await _context.SaveChangesAsync();
    }
}
