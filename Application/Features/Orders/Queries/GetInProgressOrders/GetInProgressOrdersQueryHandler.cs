using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrders
{
    public class GetInProgressOrdersQueryHandler(IOrderRepository _orderRepository, IMapper _mapper)
    : IRequestHandler<GetInProgressOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetInProgressOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllInProgressOrdersAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
