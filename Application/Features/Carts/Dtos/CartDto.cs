namespace Application.Features.Carts.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemsDto> Items { get; set; }
        public decimal TotalCartPrice => Items.Sum(item => item.TotalPrice);
    }
}
