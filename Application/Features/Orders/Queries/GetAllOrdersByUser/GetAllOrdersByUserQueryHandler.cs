using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrdersByUser
{
    public class GetAllOrdersByUserQueryHandler(IOrderRepository _orderRepository, IMapper _mapper)
        : IRequestHandler<GetAllOrdersByUserQuery, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetAllOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var orders = await _orderRepository.GetOrdersByUserNameAsync(userName);
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
