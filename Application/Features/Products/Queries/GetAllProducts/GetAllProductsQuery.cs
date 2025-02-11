using Application.Features.Products.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<ProductDto>>;
}
