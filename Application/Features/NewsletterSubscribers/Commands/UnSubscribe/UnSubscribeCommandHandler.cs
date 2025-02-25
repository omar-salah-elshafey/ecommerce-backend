using Application.ExceptionHandling;
using Application.Features.NewsletterSubscribers.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.NewsletterSubscribers.Commands.UnSubscribe
{
    public class UnSubscribeCommandHandler(INewsletterSubscriberRepository _newsletterSubscriberRepository, IMapper _mapper)
       : IRequestHandler<UnSubscribeCommand, SubscriberDto>
    {
        public async Task<SubscriberDto> Handle(UnSubscribeCommand request, CancellationToken cancellationToken)
        {
            var subscriber = await _newsletterSubscriberRepository.GetByEmailAsync(request.Email);
            if (subscriber is null)
                throw new DuplicateValueException("البريد غير صحيح");
            if (!subscriber.IsActive)
                subscriber.UnsubscribedAt = subscriber.UnsubscribedAt;
            else 
                subscriber.UnsubscribedAt = DateTime.UtcNow;
            subscriber.IsActive = false;
            await _newsletterSubscriberRepository.UpdateAsync(subscriber);
            return _mapper.Map<SubscriberDto>(subscriber);
        }
    }
}
