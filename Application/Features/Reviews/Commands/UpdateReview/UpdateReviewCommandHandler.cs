using Application.ExceptionHandling;
using Application.Features.Reviews.Dtos;
using Application.Features.TokenManagement.GetUserIdFromToken;
using Application.Features.TokenManagement.GetUserRoleFromToken;
using Application.Interfaces.IRepositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler(IReviewRepository _reviewRepository, IMapper _mapper, IValidator<UpdateReviewDto> _validator,
        IMediator _mediator) : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateReview;
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId);
            if (review == null)
                throw new NotFoundException("Review Not Found!");
            var role = await _mediator.Send(new GetUserRoleFromTokenQuery());
            var isAdmin = role.ToLower().Equals("admin");
            var currrentUserId = await _mediator.Send(new GetUserIdFromTokenQuery());
            if (!currrentUserId.Equals(review.UserId) && !isAdmin)
                throw new ForbiddenAccessException("لا يمكنك إتمام هذه العملية!");
            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            await _reviewRepository.UpdateReviewAsync(review);
            return _mapper.Map<ReviewDto>(review);
        }
    }
}
