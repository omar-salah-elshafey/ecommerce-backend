using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries.GetAllCategories;
using Application.Features.Categories.Queries.GetCategoriesCount;
using Application.Features.Categories.Queries.GetCategoryById;
using Application.Features.Categories.Queries.GetSubCategories;
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
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("get-sub-categories/{parentCategoryId}")]
        public async Task<IActionResult> GetSubCategories(Guid parentCategoryId)
        {
            var categories = await _mediator.Send(new GetSubCategoriesQuery(parentCategoryId));
            return Ok(categories);
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(category);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountAsync()
        {
            return Ok(await _mediator.Send(new GetCategoriesCountQuery()));
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
