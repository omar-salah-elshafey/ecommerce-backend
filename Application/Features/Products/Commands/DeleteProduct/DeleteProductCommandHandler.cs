using Application.ExceptionHandling;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null) 
                throw new NotFoundException("Product not found");
            await _productRepository.DeleteAsync(product);
            return Unit.Value;
        }
    }
}
