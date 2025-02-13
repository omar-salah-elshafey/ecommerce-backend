using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
