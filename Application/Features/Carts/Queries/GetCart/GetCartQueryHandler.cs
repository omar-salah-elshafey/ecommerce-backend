using Application.ExceptionHandling;
using Application.Features.Carts.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Carts.Queries.GetCart
{
    public class GetCartQueryHandler(ICartRepository _cartRepository, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetCartQuery, CartDto>
    {
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
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

            return _mapper.Map<CartDto>(cart);
        }
    }
}
