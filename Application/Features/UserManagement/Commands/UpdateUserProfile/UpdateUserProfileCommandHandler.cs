using Application.ExceptionHandling;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Features.TokenManagement.GetUserRoleFromToken;
using Application.Features.UserManagement.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserManagement.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler(UserManager<User> _userManager, ILogger<UpdateUserProfileCommandHandler> _logger, 
        IMediator _mediator, IValidator<UpdateUserDto> _validator, IMapper _mapper)
        : IRequestHandler<UpdateUserProfileCommand, UserDto>
    {
        public async Task<UserDto> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var updateUserDto = request.UpdateUserDto;
            var validationResult = await _validator.ValidateAsync(updateUserDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new NotFoundException("اسم المستخدم غير صالح!");
            var currentUserName = await _mediator.Send(new GetUsernameFromTokenQuery());
            var role = await _mediator.Send(new GetUserRoleFromTokenQuery());
            var isAdmin = role.ToLower().Equals("admin");
            _logger.LogInformation($"current UserNAme: {currentUserName}, Admin? {isAdmin}");
            if (!currentUserName.Equals(userName) && !isAdmin)
                throw new ForbiddenAccessException("لا يمكنك إتمام هذه العملية!");
            if (user.MaritalStatus != MaritalStatus.Single && updateUserDto.MaritalStatus == MaritalStatus.Single)
                throw new InvalidInputsException("لا يمكن تغيرر الحالة الاجتماعية إلى أعزب إذا كنت غير أعزب");
            _mapper.Map(updateUserDto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
                throw new Exception($"خطأ في تحديث البيانات: {errors}");
            }
            _logger.LogInformation("User has been updated Successfully.");

            var updatedUser = _mapper.Map<UserDto>(user);
            updatedUser.Role = (await _userManager.GetRolesAsync(user)).First();

            return updatedUser;
        }
    }
}
