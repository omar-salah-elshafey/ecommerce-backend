using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrdersCount
{
    public record GetInProgressOrdersCountQuery : IRequest<int>;
}
