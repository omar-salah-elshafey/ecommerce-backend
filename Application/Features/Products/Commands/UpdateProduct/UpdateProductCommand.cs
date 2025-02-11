using Application.Features.Products.Dtos;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId, UpdateProductDto ProductDto) : IRequest<ProductDto>;
}
