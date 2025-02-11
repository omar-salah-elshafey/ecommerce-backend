using Application.ExceptionHandling;
using Application.Features.Reviews.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler(IReviewRepository _reviewRepository, IMapper _mapper)
        : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        public async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(request.Id);
            if (review == null)
                throw new NotFoundException("Review not Found!");
            return _mapper.Map<ReviewDto>(review);
        }
    }
}
