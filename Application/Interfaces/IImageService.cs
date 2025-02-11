using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IImageService
    {
        Task<List<string>> SaveImagesAsync(List<IFormFile> images);
        Task DeleteImagesAsync(List<Guid> imageIds);
    }

}
