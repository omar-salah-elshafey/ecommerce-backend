using Application.Features.Carts.Dtos;
using MediatR;

namespace Application.Features.Carts.Commands.ChangeCartItem
{
    public record ChangeCartItemCommand(CartItemChangeDto CartItemChangeDto) : IRequest<CartDto>;
}
