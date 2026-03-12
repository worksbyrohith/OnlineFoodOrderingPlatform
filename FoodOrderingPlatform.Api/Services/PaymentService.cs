using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodOrderingPlatform.Api.Models;
using FoodOrderingPlatform.Api.Data;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int orderId);
        Task<Payment?> GetPaymentByOrderIdAsync(int orderId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(int orderId)
        {
            var payment = await _context.Payments.Include(p => p.Order).FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null || payment.Order == null) return false;

            if (payment.Status == "Success") return true; 

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                payment.Status = "Success";
                payment.PaidAt = DateTime.UtcNow;

                payment.Order.Status = "Confirmed";

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Payment?> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        }
    }
}
