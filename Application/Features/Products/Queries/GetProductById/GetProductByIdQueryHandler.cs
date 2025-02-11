using Application.ExceptionHandling;
using Application.Features.Products.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository _productRepository, IMapper _mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
