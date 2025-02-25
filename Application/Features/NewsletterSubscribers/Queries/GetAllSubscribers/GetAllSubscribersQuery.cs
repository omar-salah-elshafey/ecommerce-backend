using Application.Features.NewsletterSubscribers.Dtos;
using Application.Models;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Queries.GetAllSubscribers
{
    public record GetAllSubscribersQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponseModel<SubscriberDto>>;
}
