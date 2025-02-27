using Application.Features.Orders.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler(IOrderRepository _orderRepository, IMapper _mapper)
    : IRequestHandler<GetAllOrdersQuery, PaginatedResponseModel<OrderDto>>
    {
        public async Task<PaginatedResponseModel<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseModel<OrderDto>
            {
                TotalItems = orders.TotalItems,
                PageNumber = request.PageNumber,
                PageSize = orders.PageSize,
                Items = _mapper.Map<List<OrderDto>>(orders.Items)
            };
        }
    }
}
