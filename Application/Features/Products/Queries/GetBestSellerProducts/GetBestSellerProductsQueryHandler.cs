using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetBestSellerProducts
{
    public class GetBestSellerProductsQueryHandler(IProductRepository _productRepository, IMapper _mapper)
        : IRequestHandler<GetBestSellerProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetBestSellerProductsQuery request, CancellationToken cancellationToken)
        {
            var bestSeller = await _productRepository.GetBestSellerProductsAsync();
            return _mapper.Map<List<ProductDto>>(bestSeller);
        }
    }
}
