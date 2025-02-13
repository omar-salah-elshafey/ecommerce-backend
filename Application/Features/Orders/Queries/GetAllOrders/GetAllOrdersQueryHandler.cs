using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler(IOrderRepository _orderRepository, IMapper _mapper)
    : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
