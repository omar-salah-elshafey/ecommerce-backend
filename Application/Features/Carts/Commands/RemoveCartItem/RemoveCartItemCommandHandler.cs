using Application.ExceptionHandling;
using Application.Features.Carts.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Carts.Commands.RemoveCartItem
{
    public class RemoveCartItemCommandHandler(ICartRepository _cartRepository, IMapper _mapper, IMediator _mediator) 
        : IRequestHandler<RemoveCartItemCommand, CartDto>
    {
        public async Task<CartDto> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var cart = await _cartRepository.GetByUserIdAsync(userId);
            if (cart == null)
                throw new NotFoundException("Cart not found for user");

            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (itemToRemove == null)
                throw new NotFoundException("Cart item not found");

            cart.Items.Remove(itemToRemove);
            await _cartRepository.UpdateAsync(cart);

            return _mapper.Map<CartDto>(cart);
        }
    }
}
