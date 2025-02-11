using Application.Features.Products.Dtos;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(CreateProductDto CreateProductDto) : IRequest<ProductDto>;

}
