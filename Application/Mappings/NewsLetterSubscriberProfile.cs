using Application.Features.NewsletterSubscribers.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class NewsLetterSubscriberProfile : Profile
    {
        public NewsLetterSubscriberProfile()
        {
            CreateMap<NewsletterSubscriber, SubscriberDto>();
        }
    }
}
