namespace Application.Features.UsersMessages.Dtos
{
    public record MessageDto(Guid Id, string Name, string Email, string Message, DateTime MessageDate, bool IsRead);
}
