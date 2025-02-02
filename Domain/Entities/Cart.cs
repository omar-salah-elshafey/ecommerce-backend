﻿namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
