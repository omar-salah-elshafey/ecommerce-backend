using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrders
{
    public record GetInProgressOrdersQuery : IRequest<List<OrderDto>>;
}
