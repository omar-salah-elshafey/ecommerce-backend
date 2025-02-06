using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserManagement.Queries.GetUsersCount
{
    public class GetUsersCountQueryHandler(UserManager<User> _userManager): IRequestHandler<GetUsersCountQuery, int>
    {
        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.CountAsync(cancellationToken);
        }
    }
}
