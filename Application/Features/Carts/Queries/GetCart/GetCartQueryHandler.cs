using Application.ExceptionHandling;
using Application.Features.Carts.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
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
            if (cart == null)
                throw new NotFoundException("Cart not found for user");

            return _mapper.Map<CartDto>(cart);
        }
    }
}
