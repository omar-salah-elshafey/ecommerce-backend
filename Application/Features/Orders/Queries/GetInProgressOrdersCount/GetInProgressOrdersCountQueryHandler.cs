using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Orders.Queries.GetInProgressOrdersCount
{
    public class GetInProgressOrdersCountQueryHandler(IOrderRepository _orderRepository)
        : IRequestHandler<GetInProgressOrdersCountQuery, int>
    {
        public async Task<int> Handle(GetInProgressOrdersCountQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetInProgressOrdersCountAsync();
        }
    }
}
