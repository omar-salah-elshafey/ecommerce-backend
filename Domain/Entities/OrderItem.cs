﻿namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SnapShotPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
