using Domain.Enums;

namespace Application.Features.UserManagement.Dtos
{
    public record ChangeUserRoleDto(string UserName, Role Role);
}
