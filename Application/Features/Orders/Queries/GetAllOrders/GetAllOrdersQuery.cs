using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery : IRequest<List<OrderDto>>;
}
