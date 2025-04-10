﻿namespace Application.Features.Orders.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal SnapShotPrice { get; set; }
    }
}
