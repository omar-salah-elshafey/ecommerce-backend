using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.Authentication.Dtos
{
    public record RegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [JsonConverter(typeof(RoleJsonConverter))]
        public Role Role { get; set; }

        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool HasChildren { get; set; }
        public int ChildrenCount { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
