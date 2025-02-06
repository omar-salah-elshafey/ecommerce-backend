using Application.Features.UserManagement.Dtos;
using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserManagement.Queries.GetUsers
{
    public class GetUsersQueryHandler(UserManager<User> _userManager) : IRequestHandler<GetUsersQuery, PaginatedResponseModel<UserDto>>
    {
        public async Task<PaginatedResponseModel<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.pageNumber;
            var pageSize = request.pageSize;
            var totalItems = await _userManager.Users.CountAsync(cancellationToken);
            var users = await _userManager.Users
                .AsSplitQuery()
                .OrderByDescending(u => u.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userDtos.Add(new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role,
                    Gender = user.Gender.ToString(),
                    MaritalStatus = user.MaritalStatus.ToString(),
                    ChildrenCount = user.ChildrenCount,
                });
            }
            return new PaginatedResponseModel<UserDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = userDtos
            };
        }
    }
}
