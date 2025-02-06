using Domain.Enums;

namespace Application.Features.UserManagement.Dtos
{
    public record UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool HasChildren { get; set; }
        public int ChildrenCount { get; set; }
    }
}
