using Application.Features.Products.Dtos;
using MediatR;

namespace Application.Features.Products.Queries.GetBestSellerProducts
{
    public record GetBestSellerProductsQuery : IRequest<List<ProductDto>>;
}
