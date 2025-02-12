using Application.Features.Products.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetProductByCategory
{
    public record GetProductByCategoryQuery(Guid CategoryId, int PageNumber, int PageSize): IRequest<PaginatedResponseModel<ProductDto>>;
}
