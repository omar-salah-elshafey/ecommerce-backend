using Application.ExceptionHandling;
using Application.Features.Reviews.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.TokenManagement.GetUsernameFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Reviews.Commands.AddReview
{
    public class AddReviewCommandHandler(IReviewRepository _reviewRepository, IMapper _mapper, IValidator<CreateReviewDto> _validator,
            IMediator _mediator, IProductRepository _productRepository)
            : IRequestHandler<AddReviewCommand, ReviewDto>
    {
        public async Task<ReviewDto> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(new GetUserIdFromTokenQuery(), cancellationToken);
            var dto = request.CreateReviewDto;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product is null)
                throw new NotFoundException("Product Not Found!");
            if (await _reviewRepository.UserHasReview(userId, dto.ProductId))
                throw new DuplicateValueException("This user Already has a review on this product");
            var userName = await _mediator.Send(new GetUsernameFromTokenQuery(), cancellationToken);
            var review = new Review
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment.Trim(),
                CreatedAt = DateTime.UtcNow,
            };
            await _reviewRepository.AddReviewAsync(review);
            var createdReview = _mapper.Map<ReviewDto>(review) with { UserName = userName };
            return createdReview;
        }
    }
}
