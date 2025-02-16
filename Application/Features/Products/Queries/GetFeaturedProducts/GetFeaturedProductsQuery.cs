using Application.Features.Products.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetFeaturedProducts
{
    public record GetFeaturedProductsQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<ProductDto>>;
}
