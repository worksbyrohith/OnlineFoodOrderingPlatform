using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingPlatform.Api.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        
        [ForeignKey(nameof(RestaurantId))]
        public Restaurant? Restaurant { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        [Required]
        [MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}
