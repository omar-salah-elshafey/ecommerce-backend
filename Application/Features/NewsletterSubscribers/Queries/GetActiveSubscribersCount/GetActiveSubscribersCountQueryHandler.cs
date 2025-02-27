using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Queries.GetActiveSubscribersCount
{
    public class GetActiveSubscribersCountQueryHandler(INewsletterSubscriberRepository _newsletterSubscriberRepository)
        : IRequestHandler<GetActiveSubscribersCountQuery, int>
    {
        public async Task<int> Handle(GetActiveSubscribersCountQuery request, CancellationToken cancellationToken)
        {
            return await _newsletterSubscriberRepository.GetCountAsync();
        }
    }
}
