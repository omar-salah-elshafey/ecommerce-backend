using Application.ExceptionHandling;
using Application.Features.Categories.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler(IMapper _mapper, ICategoryRepository _categoryRepository, IValidator<UpdateCategoryDto> _validator) 
        : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateDto = request.UpdateCategoryDto;
            var validationResult = await _validator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) throw new NotFoundException("Category not found");
            if (updateDto.ClearParentCategory)
            {
                category.ParentCategoryId = null;
            }
            else if (updateDto.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(updateDto.ParentCategoryId.Value);
                if (parentCategory is null)
                    throw new NotFoundException("Invalid Parent Category");
                category.ParentCategoryId = updateDto.ParentCategoryId;
            }
            category.Name = updateDto.Name;
            await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
