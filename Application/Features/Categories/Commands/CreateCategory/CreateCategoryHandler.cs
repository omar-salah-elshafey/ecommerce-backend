using Application.ExceptionHandling;
using Application.Features.Categories.Dtos;
using Application.Features.UserManagement.Dtos;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler(IMapper _mapper, ICategoryRepository _categoryRepository, IValidator<CreateCategoryDto> _validator)
            : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createDto = request.CreateCategoryDto;
            var validationResult = await _validator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            if (createDto.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(createDto.ParentCategoryId.Value);
                if (parentCategory is null)
                    throw new NotFoundException("Invalid Parent Category");
            }
            var category = new Category { Id = Guid.NewGuid(), Name = createDto.Name, ParentCategoryId = createDto.ParentCategoryId };
            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
