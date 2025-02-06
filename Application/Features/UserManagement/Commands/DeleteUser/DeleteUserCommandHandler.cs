using Application.ExceptionHandling;
using Application.Features.Authentication.Commands.Logout;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Features.TokenManagement.GetUserRoleFromToken;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserManagement.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(UserManager<User> _userManager, IMediator _mediator, ILogger<DeleteUserCommandHandler> _logger) 
        : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            _logger.LogError($"userName to be deleted: ${userName}");
            _logger.LogError($"refreshToken to be deleted: ${request.refreshToken}");
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new NotFoundException("اسم المستخدم غير صالح!");
            var currentUserName = await _mediator.Send(new GetUsernameFromTokenQuery());
            var role = await _mediator.Send(new GetUserRoleFromTokenQuery());
            var isAdmin = role.ToLower().Equals("admin");
            _logger.LogInformation($"current UserName: {currentUserName}, Admin? {isAdmin}");
            if (!currentUserName.Equals(userName) && !isAdmin)
                throw new ForbiddenAccessException("لا يمكنك إتمام هذه العملية!");
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception($"حدث خطأ أثناء حذف حساب: {userName}");
            if (userName == currentUserName)
                await _mediator.Send(new LogoutCommand(request.refreshToken));
        }
    }
}
