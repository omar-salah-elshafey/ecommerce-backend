using Application.Features.NewsletterSubscribers.Dtos;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Commands.Subscirbe
{
    public record SubscirbeCommand(string Email) : IRequest<SubscriberDto>;
}
