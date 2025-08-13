using WebApi.Application.Common.Models;

public interface IShoeReadRepository
{
    Task<IEnumerable<ShoeDto>> GetAllAsync();
}
