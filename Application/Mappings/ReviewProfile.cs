using Application.Features.Reviews.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>();
        }
    }
}
