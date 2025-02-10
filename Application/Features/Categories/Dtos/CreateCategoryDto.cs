namespace Application.Features.Categories.Dtos
{
    public record CreateCategoryDto(string Name, Guid? ParentCategoryId);
}
