using Application.ExceptionHandling;
using Application.Features.NewsletterSubscribers.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Features.NewsletterSubscribers.Commands.Subscirbe
{
    public class SubscirbeCommandHandler(INewsletterSubscriberRepository _newsletterSubscriberRepository, IMapper _mapper)
        : IRequestHandler<SubscirbeCommand, SubscriberDto>
    {
        private readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        public async Task<SubscriberDto> Handle(SubscirbeCommand request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            if (!_emailRegex.IsMatch(email))
                throw new InvalidInputsException("البريد غير صالح");
            var subscriber = await _newsletterSubscriberRepository.GetByEmailAsync(email);
            if (subscriber is not null)
            {
                if (subscriber.IsActive)
                {
                    throw new DuplicateValueException("البريد مسجل بالفعل");
                }
                else
                {
                    subscriber.IsActive = true;
                    subscriber.UnsubscribedAt = null;
                    await _newsletterSubscriberRepository.UpdateAsync(subscriber);
                    return _mapper.Map<SubscriberDto>(subscriber);
                }
            }
                
            var newSubscriber = new NewsletterSubscriber
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                IsActive = true,
            };
            await _newsletterSubscriberRepository.AddAsync(newSubscriber);
            return _mapper.Map<SubscriberDto>(newSubscriber);
        }
    }
}
