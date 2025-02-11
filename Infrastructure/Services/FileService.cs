using Application.ExceptionHandling;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new InvalidInputsException("File is empty.");

            ValidateExtension(file);

            ValidateMimeType(file);

            ValidateFileSize(file);

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.UtcNow:yyyyMMddHHmmss}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/products/{uniqueFileName}";
        }
        private void ValidateExtension(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif"};
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                throw new InvalidInputsException("Invalid file type. Only image and video files are allowed.");
        }
        private void ValidateMimeType(IFormFile file)
        {
            var allowedImageMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg", "image/gif" };
            var mimeType = file.ContentType.ToLowerInvariant();
            if(!allowedImageMimeTypes.Contains(mimeType))
                throw new InvalidInputsException("Invalid file type. Only image and video files are allowed.");

        }
        private void ValidateFileSize(IFormFile file)
        {
            long maxSizeInBytes = 5 * 1024 * 1024;
            if (file.Length > maxSizeInBytes)
                throw new InvalidInputsException("File size exceeds the maximum allowed size of 5 MB.");
        }
    }

}
