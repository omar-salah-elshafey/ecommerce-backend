using Application.ExceptionHandling;
using Application.Features.UserManagement.Dtos;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserManagement.Queries.SearchUsers
{
    public class SearchUsersQueryHandler(UserManager<User> _userManager, IMapper _mapper) 
        : IRequestHandler<SearchUsersQuery, PaginatedResponseModel<UserDto>>
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

            var userDtos = _mapper.Map<List<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                var user = users.First(u => u.UserName == userDto.UserName);
                userDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
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
