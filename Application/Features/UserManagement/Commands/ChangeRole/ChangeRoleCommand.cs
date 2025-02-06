using Application.Features.UserManagement.Dtos;
using MediatR;

namespace Application.Features.UserManagement.Commands.ChangeRole
{
    public record ChangeRoleCommand(ChangeUserRoleDto changeRoleDto) : IRequest;
}
