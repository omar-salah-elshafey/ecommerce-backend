using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public record GetOrderByIdCommand(Guid OrderId) : IRequest<OrderDto>;
}
