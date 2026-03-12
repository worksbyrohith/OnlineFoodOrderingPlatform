using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FoodOrderingPlatform.Api.Services;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int id)
        {
            var items = await _menuItemService.GetMenuItemsByRestaurantAsync(id);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await _menuItemService.GetMenuItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _menuItemService.CreateMenuItemAsync(dto);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = created.ItemId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _menuItemService.UpdateMenuItemAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var deleted = await _menuItemService.DeleteMenuItemAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/availability")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var toggled = await _menuItemService.ToggleAvailabilityAsync(id);
            if (!toggled) return NotFound();

            return NoContent();
        }
    }
}
