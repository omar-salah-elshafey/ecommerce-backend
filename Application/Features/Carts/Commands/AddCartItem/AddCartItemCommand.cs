using Application.Features.Carts.Dtos;
using MediatR;

namespace Application.Features.Carts.Commands.AddCartItem
{
    public record AddCartItemCommand(CartItemChangeDto CartItemChangeDto) : IRequest<CartDto>;
}
