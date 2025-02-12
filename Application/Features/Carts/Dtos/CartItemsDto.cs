namespace Application.Features.Carts.Dtos
{
    public class CartItemsDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SnapShotPrice { get; set; }
        public decimal TotalPrice => Quantity * SnapShotPrice;
    }
}
