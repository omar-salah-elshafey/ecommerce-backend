using Application.ExceptionHandling;
using Application.Interfaces.IRepositories;
using MediatR;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler(ICategoryRepository _categoryRepository) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) throw new NotFoundException("Category not found");
            var hasSubCategories = await _categoryRepository.HasSubCategoriesAsync(category.Id);
            if (hasSubCategories)
                throw new InvalidInputsException("Cannot delete category with sub-categories. Please reassign or delete sub-categories first.");
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
