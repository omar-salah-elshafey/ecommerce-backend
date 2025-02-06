using Application.ExceptionHandling;
using Application.Features.UserManagement.Dtos;
using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserManagement.Queries.SearchUsers
{
    public class SearchUsersQueryHandler(UserManager<User> _userManager) : IRequestHandler<SearchUsersQuery, PaginatedResponseModel<UserDto>>
    {
        public async Task<PaginatedResponseModel<UserDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var searchQuery = request.searchQuery.Trim().ToLower();
            var pageNumber = request.pageNumber;
            var pageSize = request.pageSize;
            if (string.IsNullOrWhiteSpace(searchQuery))
                throw new NullOrWhiteSpaceInputException("كلمة البحث لا يمكن أن تكون فارغة!.");
            var totalItems = await _userManager.Users
                .CountAsync(u => u.UserName!.ToLower().Contains(searchQuery));
            var users = await _userManager.Users
                .Where(user => user.UserName!.ToLower().Contains(searchQuery))
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
