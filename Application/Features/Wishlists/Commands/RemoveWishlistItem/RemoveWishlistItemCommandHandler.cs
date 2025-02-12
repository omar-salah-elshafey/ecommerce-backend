using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.Wishlists.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Wishlists.Commands.RemoveWishlistItem
{
    public class RemoveWishlistItemCommandHandler(IWishlistRepository _wishlistRepository, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<RemoveWishlistItemCommand, WishlistDto>
    {
        public async Task<WishlistDto> Handle(RemoveWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
                throw new NotFoundException("Wishlist not found for user");

            var itemToRemove = wishlist.WishlistItems.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (itemToRemove == null)
                throw new NotFoundException("Wishlist item not found");

            wishlist.WishlistItems.Remove(itemToRemove);
            await _wishlistRepository.UpdateAsync(wishlist);

            return _mapper.Map<WishlistDto>(wishlist);
        }
    }
}
