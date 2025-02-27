using MediatR;

namespace Application.Features.NewsletterSubscribers.Queries.GetActiveSubscribersCount
{
    public record GetActiveSubscribersCountQuery : IRequest<int>;
}
