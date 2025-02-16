using Application.ExceptionHandling;
using Application.Features.Categories.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler(IMapper _mapper, ICategoryRepository _repository) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
