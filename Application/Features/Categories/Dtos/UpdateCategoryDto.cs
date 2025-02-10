namespace Application.Features.Categories.Dtos
{
    public record UpdateCategoryDto(string Name, Guid? ParentCategoryId, bool ClearParentCategory);
}
