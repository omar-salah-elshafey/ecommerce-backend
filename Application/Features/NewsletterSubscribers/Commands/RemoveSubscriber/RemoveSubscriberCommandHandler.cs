using Application.ExceptionHandling;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Commands.RemoveSubscriber
{
    public class RemoveSubscriberCommandHandler(INewsletterSubscriberRepository _newsletterSubscriberRepository)
        : IRequestHandler<RemoveSubscriberCommand, Unit>
    {
        public async Task<Unit> Handle(RemoveSubscriberCommand request, CancellationToken cancellationToken)
        {
            var subscriber = await _newsletterSubscriberRepository.GetByEmailAsync(request.Email);
            if (subscriber is null)
                throw new DuplicateValueException("البريد غير صحيح");
            await _newsletterSubscriberRepository.DeleteAsync(subscriber);
            return Unit.Value;
        }
    }
}
