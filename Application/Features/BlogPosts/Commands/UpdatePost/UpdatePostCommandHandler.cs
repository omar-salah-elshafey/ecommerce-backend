using Application.Features.BlogPosts.Dtos;
using Application.Interfaces.IRepositories;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Application.ExceptionHandling;
using Microsoft.Extensions.Logging;

namespace Application.Features.BlogPosts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler(IPostRepository _postRepository, IMapper _mapper,
        IValidator<UpdatePostDto> _validator, IFileService _fileService, ILogger<UpdatePostCommandHandler> _logger)
        : IRequestHandler<UpdatePostCommand, PostDto>
    {
        public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdatePostDto;

            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var postToUpdate = await _postRepository.GetByIdAsync(request.Id);
            if (postToUpdate is null)
                throw new NotFoundException("Post not Found!");

            postToUpdate.Title = dto.Title.Trim();
            postToUpdate.Content = dto.Content.Trim();
            postToUpdate.ReadTime = dto.ReadTime;

            if (dto.DeleteImage)
            {
                if (!string.IsNullOrWhiteSpace(postToUpdate.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", postToUpdate.ImageUrl.TrimStart('/'));
                    try
                    {
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error deleting file: {oldImagePath}. Exception: {ex.Message}");
                        throw;
                    }
                }
                postToUpdate.ImageUrl = null;
            }

            if (dto.DeleteVideo)
            {
                if (!string.IsNullOrWhiteSpace(postToUpdate.VideoUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", postToUpdate.VideoUrl.TrimStart('/'));
                    try
                    {
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error deleting file: {oldImagePath}. Exception: {ex.Message}");
                        throw;
                    }
                }
                postToUpdate.VideoUrl = null;
            }

            if (dto.ImageUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(dto.ImageUrl, "blogs", "image");
                postToUpdate.ImageUrl = imageUrl;
            }

            if (dto.VideoUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(dto.VideoUrl, "blogs", "video");
                postToUpdate.VideoUrl = imageUrl;
            }

            await _postRepository.UpdateAsync(postToUpdate);
            return _mapper.Map<PostDto>(postToUpdate);
        }
    }
}
