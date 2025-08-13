using WebApi.Domain.Entities.Products;

public interface IShoeWriteRepository
{
    Task AddShoeAsync(Shoe shoe);
}
