using Application.ExceptionHandling;
using Application.Features.UserManagement.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserManagement.Queries.GetUserProfile
{
    public class GetUserProfileQueryHnadler(UserManager<User> _userManager) : IRequestHandler<GetUserProfileQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userName = request.userName;
            if (string.IsNullOrEmpty(userName))
                throw new NullOrWhiteSpaceInputException("اسم المستخدم لا يمكن أن يكون فارغا!!");
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new NotFoundException("المستخدم غير موجود!!");
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = role,
                Gender = user.Gender.ToString(),
                MaritalStatus = user.MaritalStatus.ToString(),
                ChildrenCount = user.ChildrenCount,
            };
        }
    }
}
