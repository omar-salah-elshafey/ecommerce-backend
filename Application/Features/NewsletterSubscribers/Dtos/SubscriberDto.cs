namespace Application.Features.NewsletterSubscribers.Dtos
{
    public record SubscriberDto(Guid Id, string Email, bool IsActive, DateTime SubscribedAt, DateTime? UnsubscribedAt);
}
