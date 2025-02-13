using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrdersByUser
{
    public class GetInProgressOrdersByUserQueryHandler(IOrderRepository _orderRepository, IMapper _mapper, IMediator _mediator)
        : IRequestHandler<GetInProgressOrdersByUserQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetInProgressOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var orders = await _orderRepository.GetInProgressOrdersByUserNameAsync(userName);
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
