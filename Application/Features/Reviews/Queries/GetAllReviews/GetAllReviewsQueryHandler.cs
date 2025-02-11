using Application.Features.Reviews.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Reviews.Queries.GetAllReviews
{
    public class GetAllReviewsQueryHandler(IReviewRepository _reviewRepository, IMapper _mapper)
        : IRequestHandler<GetAllReviewsQuery, PaginatedResponseModel<ReviewDto>>
    {
        public async Task<PaginatedResponseModel<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetAllAsync(request.PageNumber, request.PageSize);

            return new PaginatedResponseModel<ReviewDto>
            {
                TotalItems = reviews.TotalItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Items = _mapper.Map<IEnumerable<ReviewDto>>(reviews.Items)
            };
        }
    }
}
