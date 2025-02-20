using Application.Features.Categories.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries.GetSubCategories
{
    public class GetSubCategoriesQueryHandler(IMapper _mapper, ICategoryRepository _categoryRepository)
        : IRequestHandler<GetSubCategoriesQuery, List<CategoryDto>>
    {
        public async Task<List<CategoryDto>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetSubCategotires(request.ParentCategoryId);
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
