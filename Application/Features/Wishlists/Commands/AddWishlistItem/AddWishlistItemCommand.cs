using Application.Features.Wishlists.Dtos;
using MediatR;

namespace Application.Features.Wishlists.Commands.AddWishlistItem
{
    public record AddWishlistItemCommand(Guid ProductId) : IRequest<WishlistDto>;

}
