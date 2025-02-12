using Application.Features.Carts.Dtos;
using MediatR;

namespace Application.Features.Carts.Commands.RemoveCartItem
{
    public record RemoveCartItemCommand(Guid ProductId) : IRequest<CartDto>;
}
