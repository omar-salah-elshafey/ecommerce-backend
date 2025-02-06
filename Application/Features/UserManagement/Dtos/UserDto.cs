﻿namespace Application.Features.UserManagement.Dtos
{
    public record UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int ChildrenCount { get; set; }
    }
}
