using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.Wishlists.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Wishlists.Queries.GetWishlist
{
    public class GetWishlistQueryHandler(IWishlistRepository _wishlistRepository, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetWishlistQuery, WishlistDto>
    {
        public async Task<WishlistDto> Handle(GetWishlistQuery request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
                throw new NotFoundException("Wishlist not found for user");

            return _mapper.Map<WishlistDto>(wishlist);
        }
    }
}
