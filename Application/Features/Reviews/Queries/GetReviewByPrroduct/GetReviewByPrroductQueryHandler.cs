using Application.ExceptionHandling;
using Application.Features.Reviews.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Reviews.Queries.GetReviewByPrroduct
{
    public class GetReviewByPrroductQueryHandler(IReviewRepository _reviewRepository, IMapper _mapper, IProductRepository _productRepository)
        : IRequestHandler<GetReviewByPrroductQuery, PaginatedResponseModel<ReviewDto>>
    {
        public async Task<PaginatedResponseModel<ReviewDto>> Handle(GetReviewByPrroductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
                throw new NotFoundException("Product not found.");
            var reviews = await _reviewRepository.GetReviewsByProductIdAsync(request.ProductId, request.PageNumber, request.PageSize);
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
