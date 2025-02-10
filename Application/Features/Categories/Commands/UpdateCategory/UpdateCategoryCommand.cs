using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, UpdateCategoryDto UpdateCategoryDto) : IRequest<CategoryDto>;
}
