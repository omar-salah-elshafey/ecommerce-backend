using Application.Features.BlogPosts.Commands.CreatePost;
using Application.Features.BlogPosts.Commands.DeletePost;
using Application.Features.BlogPosts.Commands.UpdatePost;
using Application.Features.BlogPosts.Dtos;
using Application.Features.BlogPosts.Queries.GetAllPosts;
using Application.Features.BlogPosts.Queries.GetPostById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("create-post")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreatePostAsync([FromForm] CreatePostDto createPostDto)
        {
            var result = await _mediator.Send(new CreatePostCommand(createPostDto));
            return Ok(result);
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPostsAsync(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllPostsQuery(pageNumber, pageSize));
            return Ok(result);
        }

        [HttpGet("posts/{id}")]
        public async Task<IActionResult> GetAllPostByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("update-post/{id}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdatePostAsync(Guid id, [FromForm] UpdatePostDto updatePostDto)
        {
            var result = await _mediator.Send(new UpdatePostCommand(id, updatePostDto));
            return Ok(result);
        }

        [HttpDelete("delete-post/{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return NoContent();
        }
    }
}
