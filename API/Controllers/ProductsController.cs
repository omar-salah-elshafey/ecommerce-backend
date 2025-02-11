using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Dtos;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync([FromForm] CreateProductDto createProductDto)
        {
            var product = await _mediator.Send(new CreateProductCommand(createProductDto));
            return Ok(product);
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProductsAsync(int PageNumber = 1, int PageSize = 10)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(PageNumber, PageSize));
            return Ok(products);
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetProductsByIdsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromForm] UpdateProductDto updateProductDto)
        {
            var product = await _mediator.Send(new UpdateProductCommand(id, updateProductDto));
            return Ok(product);
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
