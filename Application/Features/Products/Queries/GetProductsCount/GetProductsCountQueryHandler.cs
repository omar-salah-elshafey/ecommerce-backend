using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Products.Queries.GetProductsCount
{
    public class GetProductsCountQueryHandler(IProductRepository _productRepository) : IRequestHandler<GetProductsCountQuery, int>
    {
        public async Task<int> Handle(GetProductsCountQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetCountAsync();
        }
    }
}
