using System;

namespace FoodOrderingPlatform.Api.DTOs
{
    public class DashboardDto
    {
        public int TotalOrdersToday { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveRestaurantsCount { get; set; }
        public int PendingOrdersCount { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
