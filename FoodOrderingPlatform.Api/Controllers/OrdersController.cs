using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FoodOrderingPlatform.Api.Services;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires auth for all endpoints inside
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var order = await _orderService.PlaceOrderAsync(GetUserId(), dto);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _orderService.GetMyOrdersAsync(GetUserId());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            // Only allow Admin or the order owner to view it
            if (!User.IsInRole("Admin") && order.UserId != GetUserId())
                return Forbid();

            return Ok(order);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] string? status, [FromQuery] DateTime? date)
        {
            var orders = await _orderService.GetAllOrdersAsync(status, date);
            return Ok(orders);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderUpdateStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _orderService.UpdateOrderStatusAsync(id, dto.Status);
            if (!updated) return NotFound();

            return NoContent();
        }
    }
}
