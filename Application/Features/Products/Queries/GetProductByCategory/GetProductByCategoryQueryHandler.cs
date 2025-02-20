using Application.ExceptionHandling;
using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetProductByCategory
{
    public class GetProductByCategoryQueryHandler(IProductRepository _productRepository, IMapper _mapper, ICategoryRepository _categoryRepository)
        : IRequestHandler<GetProductByCategoryQuery, PaginatedResponseModel<ProductDto>>
    {
        public async Task<PaginatedResponseModel<ProductDto>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            //var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            //if (category is null)
            //    throw new NotFoundException("Category Not Found!");
            var paginatedProducts = await _productRepository.GetByCategoryIdAsync(request.CategoryIds, request.PageNumber, request.PageSize);
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
