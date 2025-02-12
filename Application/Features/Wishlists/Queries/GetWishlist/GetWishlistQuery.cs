using Application.Features.Wishlists.Dtos;
using MediatR;

namespace Application.Features.Wishlists.Queries.GetWishlist
{
    public record GetWishlistQuery() : IRequest<WishlistDto>;

}
