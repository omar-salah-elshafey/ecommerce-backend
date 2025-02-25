using Application.Features.NewsletterSubscribers.Dtos;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Commands.UnSubscribe
{
    public record UnSubscribeCommand(string Email) : IRequest<SubscriberDto>;
}
