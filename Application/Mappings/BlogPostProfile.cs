using Application.Features.BlogPosts.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, PostDto>();
        }
    }
}
