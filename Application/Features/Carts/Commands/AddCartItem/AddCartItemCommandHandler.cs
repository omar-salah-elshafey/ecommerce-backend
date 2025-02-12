using Application.ExceptionHandling;
using Application.Features.Carts.Dtos;
using Application.Features.Products.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Carts.Commands.AddCartItem
{
    public class AddCartItemCommandHandler(ICartRepository _cartRepository, IProductRepository _productRepository,
        IMapper _mapper, ILogger<AddCartItemCommandHandler> _logger, IMediator _mediator, IValidator<CartItemChangeDto> _validator)
        : IRequestHandler<AddCartItemCommand, CartDto>
    {
        public async Task<CartDto> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var createDto = request.CartItemChangeDto;
            var validationResult = await _validator.ValidateAsync(createDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var product = await _productRepository.GetByIdAsync(createDto.productId);
            if (product == null)
                throw new NotFoundException("Product not found");
            var cart = await _cartRepository.GetByUserIdAsync(userId);
            if (cart is null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                await _cartRepository.AddAsync(cart);
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == createDto.productId);
            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = createDto.productId,
                    CartId = cart.Id,
                    SnapShotPrice = product.Price,
                    Quantity = createDto.quantity,
                };
                cart.Items.Add(cartItem);
                await _cartRepository.AddItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity = createDto.quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
            return _mapper.Map<CartDto>(cart);
        }
    }
}
