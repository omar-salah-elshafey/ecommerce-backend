using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(CreateCategoryDto CreateCategoryDto) : IRequest<CategoryDto>;
}
