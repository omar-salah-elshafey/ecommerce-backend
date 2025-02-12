namespace Application.Features.Carts.Dtos
{
    public record CartItemChangeDto(Guid productId, int quantity);
}
