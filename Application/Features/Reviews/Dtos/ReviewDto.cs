namespace Application.Features.Reviews.Dtos
{
    public record ReviewDto(Guid Id, Guid ProductId, string UserName, int Rating, string Comment, DateTime CreatedAt);
}
