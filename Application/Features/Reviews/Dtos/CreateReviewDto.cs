namespace Application.Features.Reviews.Dtos
{
    public record CreateReviewDto(Guid ProductId, int Rating, string Comment);
}
