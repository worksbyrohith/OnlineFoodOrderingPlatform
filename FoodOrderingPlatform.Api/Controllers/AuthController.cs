using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FoodOrderingPlatform.Api.DTOs;
using FoodOrderingPlatform.Api.Services;

namespace FoodOrderingPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(registerDto);
            if (!result)
                return BadRequest(new { Message = "Email already exists" });

            return StatusCode(201, new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.LoginAsync(loginDto);
            if (response == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            return Ok(response);
        }
    }
}
