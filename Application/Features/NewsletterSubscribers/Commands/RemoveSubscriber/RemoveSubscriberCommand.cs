using MediatR;

namespace Application.Features.NewsletterSubscribers.Commands.RemoveSubscriber
{
    public record RemoveSubscriberCommand(string Email) : IRequest<Unit>;
}
