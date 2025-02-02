using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool HasChildren { get; set; }
        public int ChildrenCount { get; set; }
        public bool Online { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<RefreshToken>? RefreshTokens { get; set; }
        public Cart Cart { get; set; }
    }
}
