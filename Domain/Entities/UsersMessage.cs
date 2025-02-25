using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UsersMessage
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime MessageDate { get; set; }
    }
}
