using Application.ExceptionHandling;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file, string targetFolder, string expectedType)
        {
            if (file == null || file.Length == 0)
                throw new InvalidInputsException("File is empty.");

            ValidateExtension(file, expectedType);

            ValidateMimeType(file, expectedType);

            ValidateFileSize(file);

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", targetFolder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.UtcNow:yyyyMMddHHmmss}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{targetFolder}/{uniqueFileName}";
        }
        private void ValidateExtension(IFormFile file, string expectedType)
        {
            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif"};
            var allowedVideoExtensions = new[] { ".mp4", ".avi", ".mov" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (expectedType == "image" && !allowedImageExtensions.Contains(fileExtension))
                throw new InvalidInputsException("Invalid file type. Only JPG, JPEG, PNG, or GIF files are allowed.");
            if (expectedType == "video" && !allowedVideoExtensions.Contains(fileExtension))
                throw new InvalidInputsException("Invalid file type. Only MP4, AVI, or MOV files are allowed.");
        }
        private void ValidateMimeType(IFormFile file, string expectedType)
        {
            var allowedImageMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg", "image/gif" };
            var allowedVideoMimeTypes = new[] { "video/mp4", "video/avi", "video/quicktime" };
            var mimeType = file.ContentType.ToLowerInvariant();
            if (expectedType == "image" && !allowedImageMimeTypes.Contains(file.ContentType))
                throw new InvalidInputsException("Invalid MIME type. Only image files are allowed.");
            if (expectedType == "video" && !allowedVideoMimeTypes.Contains(file.ContentType))
                throw new InvalidInputsException("Invalid MIME type. Only video files are allowed.");

        }
        private void ValidateFileSize(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            long maxSizeInBytes = fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov"
                ? 50 * 1024 * 1024
                : 5 * 1024 * 1024;
            if (file.Length > maxSizeInBytes)
                throw new InvalidInputsException($"File size exceeds the maximum allowed size of {maxSizeInBytes / (1024 * 1024)} MB.");
        }
    }

}
