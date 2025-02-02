using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.Authentication.Dtos
{
    public record RegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool HasChildren { get; set; }
        public int ChildrenCount { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
