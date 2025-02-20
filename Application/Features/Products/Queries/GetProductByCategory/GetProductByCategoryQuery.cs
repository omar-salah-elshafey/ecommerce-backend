using Application.Features.Products.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetProductByCategory
{
    public record GetProductByCategoryQuery(List<Guid> CategoryIds, int PageNumber, int PageSize): IRequest<PaginatedResponseModel<ProductDto>>;
}
