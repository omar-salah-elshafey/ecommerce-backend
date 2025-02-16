using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.Wishlists.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
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
            if (wishlist is null)
            {
                wishlist = new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    WishlistItems = new List<WishlistItem>()
                };
                await _wishlistRepository.AddAsync(wishlist);
            }

            return _mapper.Map<WishlistDto>(wishlist);
        }
    }
}
