using Application.ExceptionHandling;
using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdCommandHandler(IOrderRepository _orderRepository, IMapper _mapper) : IRequestHandler<GetOrderByIdCommand, OrderDto>
    {
        public async Task<OrderDto> Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order is null)
                throw new NotFoundException("Order not found!");
            return _mapper.Map<OrderDto>(order);
        }
    }
}
