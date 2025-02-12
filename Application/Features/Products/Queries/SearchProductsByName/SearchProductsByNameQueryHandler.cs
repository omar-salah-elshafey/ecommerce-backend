using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.SearchProductsByName
{
    public class SearchProductsByNameQueryHandler(IProductRepository _productRepository, IMapper _mapper)
        : IRequestHandler<SearchProductsByNameQuery, PaginatedResponseModel<ProductDto>>
    {
        public async Task<PaginatedResponseModel<ProductDto>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var paginatedProducts = await _productRepository.GetByNameAsync(request.Query, request.PageNumber, request.PageSize);
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
