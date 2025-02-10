using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries.GetAllCategories;
using Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery(pageNumber, pageSize));
            return Ok(categories);
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }

        [HttpPost("add-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var category = await _mediator.Send(new CreateCategoryCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("update-category/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id,UpdateCategoryDto dto)
        {
            var category = await _mediator.Send(new UpdateCategoryCommand(id, dto));
            return Ok(category);
        }

        [HttpDelete("delete-category/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
