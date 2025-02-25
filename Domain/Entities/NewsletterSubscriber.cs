using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class NewsletterSubscriber
    {
        public Guid Id { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        public bool IsActive { get; set; } = false;

        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UnsubscribedAt { get; set; }
    }

}
