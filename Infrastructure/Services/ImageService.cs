using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string _uploadsFolder;

        public ImageService(IConfiguration configuration)
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");

            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<List<string>> SaveImagesAsync(List<IFormFile> images)
        {
            var imagePaths = new List<string>();

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var fileExtension = Path.GetExtension(image.FileName);

                    var uniqueFileName = $"{Guid.NewGuid()}_{DateTime.UtcNow.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)}{fileExtension}";
                    var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    imagePaths.Add($"/uploads/products/{uniqueFileName}");
                }
            }

            return imagePaths;
        }

        public async Task DeleteImagesAsync(List<Guid> imageIds)
        {
            var files = Directory.GetFiles(_uploadsFolder);

            foreach (var imageId in imageIds)
            {
                var idStr = imageId.ToString();

                var fileToDelete = files.FirstOrDefault(file =>
                    Path.GetFileName(file).StartsWith(idStr, StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrEmpty(fileToDelete))
                {
                    try
                    {
                        File.Delete(fileToDelete);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}
