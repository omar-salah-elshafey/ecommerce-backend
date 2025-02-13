using Application.ExceptionHandling;
using Application.Features.Carts.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Carts.Commands.ChangeCartItem
{
    public class ChangeCartItemCommandHandler(ICartRepository _cartRepository, IProductRepository _productRepository,
        IMapper _mapper, IMediator _mediator, IValidator<CartItemChangeDto> _validator) : IRequestHandler<ChangeCartItemCommand, CartDto>
    {
        public async Task<CartDto> Handle(ChangeCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var updateDto = request.CartItemChangeDto;
            var validationResult = await _validator.ValidateAsync(updateDto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var product = await _productRepository.GetByIdAsync(updateDto.productId);
            if (product == null)
                throw new NotFoundException("Product not found");
            if (updateDto.quantity > product.MaxOrderQuantity)
                throw new InvalidInputsException($"You can only order up to {product.MaxOrderQuantity} units.");
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

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == updateDto.productId);
            if(cartItem is null)
            {
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = updateDto.productId,
                    CartId = cart.Id,
                    SnapShotPrice = product.Price,
                    Quantity = updateDto.quantity,
                };
                cart.Items.Add(cartItem);
                await _cartRepository.AddItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity = updateDto.quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }

            return _mapper.Map<CartDto>(cart);
        }
    }
}
