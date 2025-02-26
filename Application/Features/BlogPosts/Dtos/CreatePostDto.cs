using Microsoft.AspNetCore.Http;

namespace Application.Features.BlogPosts.Dtos
{
    public record CreatePostDto(string Title, string Content, IFormFile? ImageUrl, IFormFile? VideoUrl, int ReadTime);
}
