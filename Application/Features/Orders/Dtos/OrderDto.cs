namespace Application.Features.Orders.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public Guid AddressId { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string UserName { get; set; }
    }
}
