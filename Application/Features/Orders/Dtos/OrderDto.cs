using Domain.Enums;

namespace Application.Features.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public Guid AddressId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string UserName { get; set; }
    }
}
