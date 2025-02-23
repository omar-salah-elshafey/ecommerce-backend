using Application.Features.Orders.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetUserAddresses
{
    public class GetUserAddressesQueryHandler(IOrderRepository _orderRepository, IMapper _mapper, IMediator _mediator) 
        : IRequestHandler<GetUserAddressesQuery, List<AddressDto>>
    {
        public async Task<List<AddressDto>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery());
            var addresses = await _orderRepository.GetUserAddressesAsync(userId);
            return _mapper.Map<List<AddressDto>>(addresses);
        }
    }
}
