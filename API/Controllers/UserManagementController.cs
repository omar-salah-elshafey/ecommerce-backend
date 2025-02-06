using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Features.UserManagement.Commands.ChangeRole;
using Application.Features.UserManagement.Commands.DeleteUser;
using Application.Features.UserManagement.Commands.UpdateUserProfile;
using Application.Features.UserManagement.Dtos;
using Application.Features.UserManagement.Queries.GetUserProfile;
using Application.Features.UserManagement.Queries.GetUsers;
using Application.Features.UserManagement.Queries.GetUsersCount;
using Application.Features.UserManagement.Queries.SearchUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController(IMediator _mediator) : ControllerBase
    {

        [HttpGet("get-users-count")]
        public async Task<IActionResult> GetUsersCountAsync()
        {
            var usersCount = await _mediator.Send(new GetUsersCountQuery());
            return Ok(new { count = usersCount });
        }
        [HttpGet("get-all-users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _mediator.Send(new GetUsersQuery(pageNumber, pageSize));
            return Ok(users);
        }

        [HttpGet("get-current-user-profile")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserProfileAsync()
        {
            var currentUserName = await _mediator.Send(new GetUsernameFromTokenQuery());
            var userProfile = await _mediator.Send(new GetUserProfileQuery(currentUserName));
            return Ok(userProfile);
        }

        [HttpGet("get-user-profile/{userName}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfileAsync(string userName)
        {
            var user = await _mediator.Send(new GetUserProfileQuery(userName));
            return Ok(user);
        }

        [HttpGet("search-users/{query}")]
        [Authorize]
        public async Task<IActionResult> SearchUsersAsync(string query, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new SearchUsersQuery(query, pageNumber, pageSize));
            return Ok(result);
        }

        [HttpPut("update-user/{userName}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(string userName, UpdateUserDto updateUserDto)
        {
            var result = await _mediator.Send(new UpdateUserProfileCommand(userName, updateUserDto));
            return Ok(result);
        }

        [HttpPut("change-user-role")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ChangeUserRoleAsync(ChangeUserRoleDto changeRoleDto)
        {
            await _mediator.Send(new ChangeRoleCommand(changeRoleDto));
            return Ok(new { message = $"المستخدم {changeRoleDto.UserName} تم إسناد الصلاحيات الجديدية له بنجاح {changeRoleDto.Role.ToString().ToUpper()} له بنجاح :)" });
        }

        [HttpDelete("delete-user/{userName}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(string userName, string? refreshToken)
        {
            if (refreshToken == null)
                refreshToken = Request.Cookies["refreshToken"];
            await _mediator.Send(new DeleteUserCommand(userName, refreshToken));
            return Ok(new { message = $"تم حذف الحساب بنجاح: '{userName}'." });
        }
    }
}
