using MediatR;
using WebApi.Application.Common.Models;

namespace WebApi.Application.Queries.Shoes;

public record GetAllShoesQuery() : IRequest<IEnumerable<ShoeDto>>;