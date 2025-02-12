using Application.Features.Wishlists.Dtos;
using MediatR;

namespace Application.Features.Wishlists.Commands.RemoveWishlistItem
{
    public record RemoveWishlistItemCommand(Guid ProductId) : IRequest<WishlistDto>;

}
