using Application.ExceptionHandling;
using Application.Features.UserManagement.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserManagement.Queries.GetUserProfile
{
    public class GetUserProfileQueryHnadler(UserManager<User> _userManager, IMapper _mapper) : IRequestHandler<GetUserProfileQuery, UserDto>
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
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = role;
            return userDto;
        }
    }
}
