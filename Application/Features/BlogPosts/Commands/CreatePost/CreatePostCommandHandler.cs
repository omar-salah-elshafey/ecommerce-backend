using Application.Features.BlogPosts.Dtos;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.BlogPosts.Commands.CreatePost
{
    public class CreatePostCommandHandler(IPostRepository _postRepository, IMapper _mapper, 
        IValidator<CreatePostDto> _validator, IFileService _fileService)
        : IRequestHandler<CreatePostCommand, PostDto>
    {
        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreatePostDto;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var post = new BlogPost
            {
                Id = Guid.NewGuid(),
                Title = dto.Title.Trim(),
                Content = dto.Content.Trim(),
                PublishDate = DateTime.UtcNow,
                ReadTime = dto.ReadTime,
            };
            if(dto.ImageUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(dto.ImageUrl, "blogs", "image");
                post.ImageUrl = imageUrl;
            }
            if (dto.VideoUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(dto.VideoUrl, "blogs", "video");
                post.VideoUrl = imageUrl;
            }
            await _postRepository.AddAsync(post);
            return _mapper.Map<PostDto>(post);
        }
    }
}
