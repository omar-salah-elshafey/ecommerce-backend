using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersCount
{
    public record GetOrdersCountQuery : IRequest<int>;
}
