using Application.Features.Orders.Dtos;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrdersByUser
{
    public record GetInProgressOrdersByUserQuery(string UserName) : IRequest<List<OrderDto>>;
}
