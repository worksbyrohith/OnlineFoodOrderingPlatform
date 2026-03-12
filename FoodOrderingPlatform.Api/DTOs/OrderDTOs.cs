using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingPlatform.Api.DTOs
{
    public class OrderItemCreateDto
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }

    public class OrderCreateDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        [MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }

    public class OrderUpdateStatusDto
    {
        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = string.Empty; // Confirmed, Preparing, Out for Delivery, Delivered
    }
}
