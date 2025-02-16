using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Features.Wishlists.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Wishlists.Commands.AddWishlistItem
{
    public class AddWishlistItemCommandHandler(IWishlistRepository _wishlistRepository, IProductRepository _productRepository,
        IMapper _mapper, ILogger<AddWishlistItemCommandHandler> _logger, IMediator _mediator) : IRequestHandler<AddWishlistItemCommand, WishlistDto>
    {
        public async Task<WishlistDto> Handle(AddWishlistItemCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    WishlistItems = new List<WishlistItem>()
                };
                await _wishlistRepository.AddAsync(wishlist);
            }

            if (wishlist.WishlistItems.Any(item => item.ProductId == request.ProductId))
                throw new DuplicateValueException("Product is already in the wishlist");

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new NotFoundException("Product not found");

            var wishlistItem = new WishlistItem
            {
                Id = Guid.NewGuid(),
                WishlistId = wishlist.Id,
                ProductId = product.Id,
                AddedAt = DateTime.UtcNow,
            };
            wishlist.WishlistItems.Add(wishlistItem);
            await _wishlistRepository.AddItemAsync(wishlistItem);
            return _mapper.Map<WishlistDto>(wishlist);
        }
    }
}
