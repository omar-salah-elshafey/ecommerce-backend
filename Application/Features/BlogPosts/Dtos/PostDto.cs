namespace Application.Features.BlogPosts.Dtos
{
    public record PostDto(Guid Id, string Title, string Content, DateTime PublishDate, string? ImageUrl, string? VideoUrl, int ReadTime);
}
