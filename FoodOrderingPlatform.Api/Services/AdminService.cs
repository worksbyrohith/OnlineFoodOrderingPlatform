using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodOrderingPlatform.Api.Data;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IAdminService
    {
        Task<DashboardDto> GetDashboardStatsAsync();
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }

    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardStatsAsync()
        {
            var today = DateTime.UtcNow.Date;

            var totalOrdersToday = await _context.Orders
                .Where(o => o.OrderedAt >= today)
                .CountAsync();

            var totalRevenue = await _context.Payments
                .Where(p => p.Status == "Success")
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var activeRestaurants = await _context.Restaurants
                .Where(r => r.IsActive)
                .CountAsync();

            var pendingOrders = await _context.Orders
                .Where(o => o.Status == "Pending")
                .CountAsync();

            return new DashboardDto
            {
                TotalOrdersToday = totalOrdersToday,
                TotalRevenue = totalRevenue,
                ActiveRestaurantsCount = activeRestaurants,
                PendingOrdersCount = pendingOrders
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role ?? "Customer",
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }
    }
}
