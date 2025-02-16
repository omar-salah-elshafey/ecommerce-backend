using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId) : IRequest<OrderDto>;
}
