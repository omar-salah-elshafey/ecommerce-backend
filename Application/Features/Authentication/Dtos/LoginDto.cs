using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentication.Dtos
{
    public record LoginDto
    {
        [Required, MaxLength(50)]
        public string EmailOrUserName { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
    }
}
