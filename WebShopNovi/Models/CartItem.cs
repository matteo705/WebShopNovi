﻿namespace WebShopNovi.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public string ImageFileName { get; set; }
    }
}
