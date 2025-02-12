using Application.Features.Products.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.Products.Queries.SearchProductsByName
{
    public record SearchProductsByNameQuery(string Query, int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<ProductDto>>;
}
