using Application.ExceptionHandling;
using Application.Features.UserManagement.Dtos;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.UserManagement.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, 
        IValidator<ChangeUserRoleDto> _validator, ILogger<ChangeRoleCommandHandler> _logger) 
        : IRequestHandler<ChangeRoleCommand>
    {
        public async Task Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var changeRoleDto = request.changeRoleDto;
            var validationResult = await _validator.ValidateAsync(changeRoleDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var user = await _userManager.FindByNameAsync(changeRoleDto.UserName);
            if (user is null)
                throw new NotFoundException("اسم المستخدم غير صالح!");
            if (await _userManager.IsInRoleAsync(user, changeRoleDto.Role.ToString()))
                throw new DuplicateValueException("هذا المستخدم بالفعل له هذه الصلاحيات!");

            var currentrole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            await _userManager.RemoveFromRoleAsync(user, currentrole);
            var result = await _userManager.AddToRoleAsync(user, changeRoleDto.Role.ToString());
            if (!result.Succeeded)
                throw new Exception("حدث خطأ أثناء إتمام العملية!");
            _logger.LogInformation($"User {changeRoleDto.UserName} has been assignd to Role {changeRoleDto.Role.ToString().ToUpper()} Successfully :)");

        }
    }
}
