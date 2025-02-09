using Application.Features.UserManagement.Dtos;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserManagement.Queries.GetUsers
{
    public class GetUsersQueryHandler(UserManager<User> _userManager, IMapper _mapper) : IRequestHandler<GetUsersQuery, PaginatedResponseModel<UserDto>>
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
