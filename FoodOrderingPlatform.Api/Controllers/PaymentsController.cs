using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FoodOrderingPlatform.Api.Services;

namespace FoodOrderingPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process/{orderId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ProcessPayment(int orderId)
        {
            var success = await _paymentService.ProcessPaymentAsync(orderId);
            if (!success) return BadRequest(new { Message = "Payment failed or order not found." });

            return Ok(new { Message = "Payment successful." });
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetPayment(int orderId)
        {
            var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);
            if (payment == null) return NotFound();

            return Ok(payment);
        }
    }
}
