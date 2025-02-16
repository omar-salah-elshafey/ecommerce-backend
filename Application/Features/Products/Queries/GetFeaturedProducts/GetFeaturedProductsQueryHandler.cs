using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetFeaturedProducts
{
    public class GetFeaturedProductsQueryHandler(IProductRepository _productRepository, IMapper _mapper)
        : IRequestHandler<GetFeaturedProductsQuery, PaginatedResponseModel<ProductDto>>
    {
        public async Task<PaginatedResponseModel<ProductDto>> Handle(GetFeaturedProductsQuery request, CancellationToken cancellationToken)
        {
            var paginatedProducts = await _productRepository.GetFeaturedProductsAsync(request.PageNumber, request.PageSize);

            return new PaginatedResponseModel<ProductDto>
            {
                TotalItems = paginatedProducts.TotalItems,
                PageNumber = paginatedProducts.PageNumber,
                PageSize = paginatedProducts.PageSize,
                Items = _mapper.Map<IEnumerable<ProductDto>>(paginatedProducts.Items)
            };
        }
    }
}
