﻿namespace Application.Features.Categories.Dtos
{
    public record CategoryDto(Guid Id, string Name, Guid? ParentCategoryId, string? ParentCategoryName, List<CategoryDto>? SubCategories);
}
