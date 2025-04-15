namespace Domain.Entities
{
    public class OrderNotification
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProcessed { get; set; }
        public Order Order { get; set; }
    }
}
