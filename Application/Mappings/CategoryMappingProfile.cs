using Application.Features.Categories.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
