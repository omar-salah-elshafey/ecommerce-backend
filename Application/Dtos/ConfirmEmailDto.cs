using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public record ConfirmEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
