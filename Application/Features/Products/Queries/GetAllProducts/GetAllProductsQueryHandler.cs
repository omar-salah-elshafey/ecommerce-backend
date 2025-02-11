using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IProductRepository _productRepository, IMapper _mapper)
        : IRequestHandler<GetAllProductsQuery, PaginatedResponseModel<ProductDto>>
    {
        public async Task<PaginatedResponseModel<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var paginatedProducts = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize);

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
