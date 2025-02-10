using Application.Features.Categories.Dtos;
using Application.Interfaces.IRepositories;
using Application.Models;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler(IMapper _mapper, ICategoryRepository _categoryRepository) 
        : IRequestHandler<GetAllCategoriesQuery, PaginatedResponseModel<CategoryDto>>
    {
        public async Task<PaginatedResponseModel<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseModel<CategoryDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = categories.TotalItems,
                Items = _mapper.Map<IEnumerable<CategoryDto>>(categories.Items)
            };
        }
    }
}
