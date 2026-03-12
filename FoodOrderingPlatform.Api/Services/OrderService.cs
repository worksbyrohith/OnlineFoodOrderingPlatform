using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodOrderingPlatform.Api.Models;
using FoodOrderingPlatform.Api.Data;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(int userId, OrderCreateDto dto);
        Task<IEnumerable<Order>> GetMyOrdersAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync(string? status, DateTime? date);
        Task<bool> UpdateOrderStatusAsync(int id, string status);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrderAsync(int userId, OrderCreateDto dto)
        {
            // Transaction started
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal totalAmount = 0;
                var orderItems = new List<OrderItem>();

                foreach (var itemDto in dto.Items)
                {
                    var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.ItemId == itemDto.ItemId);
                    if (menuItem == null || !menuItem.IsAvailable || menuItem.RestaurantId != dto.RestaurantId)
                    {
                        throw new InvalidOperationException($"Item {itemDto.ItemId} is invalid or unavailable.");
                    }

                    // Concurrency test point: if we had a stock field, we would check and update it here
                    totalAmount += menuItem.Price * itemDto.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        ItemId = menuItem.ItemId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = menuItem.Price
                    });
                }

                var order = new Order
                {
                    UserId = userId,
                    RestaurantId = dto.RestaurantId,
                    TotalAmount = totalAmount,
                    Status = "Pending",
                    DeliveryAddress = dto.DeliveryAddress,
                    OrderedAt = DateTime.UtcNow,
                    OrderItems = orderItems,
                    Payment = new Payment
                    {
                        Amount = totalAmount,
                        Method = "COD", // Default, simulating payment later updates this map
                        Status = "Pending"
                    }
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync(); // Saves Order, OrderItems, Payment in one go

                await transaction.CommitAsync();
                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetMyOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
                .Include(o => o.Payment)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string? status, DateTime? date)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            if (date.HasValue)
                query = query.Where(o => o.OrderedAt.Date == date.Value.Date);

            return await query.OrderByDescending(o => o.OrderedAt).ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
