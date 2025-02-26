using Microsoft.AspNetCore.Http;

namespace Application.Features.BlogPosts.Dtos
{
    public record UpdatePostDto(string Title, string Content, IFormFile? ImageUrl, IFormFile? VideoUrl, 
        int ReadTime, bool DeleteImage = false, bool DeleteVideo = false);
}
