using Application.Features.Reviews.Commands.AddReview;
using Application.Features.Reviews.Commands.DeleteReview;
using Application.Features.Reviews.Commands.UpdateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Queries.GetAllReviews;
using Application.Features.Reviews.Queries.GetReviewById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-review")]
        [Authorize]
        public async Task<IActionResult> AddReviewAsync(CreateReviewDto createReviewDto)
        {
            var review = await _mediator.Send(new AddReviewCommand(createReviewDto));
            return Ok(review);
        }
        
        [HttpGet("get-all-reviews")]
        public async Task<IActionResult> GetAllReviewsAsync(int pageNumber = 1,  int pageSize = 10)
        {
            var reviews = await _mediator.Send(new GetAllReviewsQuery(pageNumber, pageSize));
            return Ok(reviews);
        }

        [HttpGet("get-review/{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(Guid id)
        {
            var review = await _mediator.Send(new GetReviewByIdQuery(id));
            return Ok(review);
        }

        [HttpPut("update-review/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(Guid id, UpdateReviewDto updateReviewDto)
        {
            var review = await _mediator.Send(new UpdateReviewCommand(id, updateReviewDto));
            return Ok(review);
        }

        [HttpDelete("delete-review/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReviewAsync(Guid id)
        {
            await _mediator.Send(new DeleteReviewCommand(id));
            return NoContent();
        }
    }
}
