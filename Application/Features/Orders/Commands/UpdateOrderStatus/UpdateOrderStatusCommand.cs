using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Commands.UpdateOrderStatus
{
    public record UpdateOrderStatusCommand(UpdateOrderStatusDto UpdateOrderStatusDto) : IRequest<OrderDto>;

}
