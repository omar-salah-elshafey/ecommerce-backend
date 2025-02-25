using Application.Features.NewsletterSubscribers.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Queries.GetAllSubscribers
{
    public class GetAllSubscribersQueryHandler(INewsletterSubscriberRepository _newsletterSubscriberRepository, IMapper _mapper)
        : IRequestHandler<GetAllSubscribersQuery, PaginatedResponseModel<SubscriberDto>>
    {
        public async Task<PaginatedResponseModel<SubscriberDto>> Handle(GetAllSubscribersQuery request, CancellationToken cancellationToken)
        {
            var subscribers = await _newsletterSubscriberRepository.GetAllAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseModel<SubscriberDto>
            {
                PageNumber = subscribers.PageNumber,
                PageSize = subscribers.PageSize,
                TotalItems = subscribers.TotalItems,
                Items = _mapper.Map<IEnumerable<SubscriberDto>>(subscribers.Items)
            };
        }
    }
}
