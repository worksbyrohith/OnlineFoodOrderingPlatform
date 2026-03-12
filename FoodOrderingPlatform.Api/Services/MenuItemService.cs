using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrderingPlatform.Api.Models;
using FoodOrderingPlatform.Api.Repositories;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantAsync(int restaurantId);
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task<MenuItem> CreateMenuItemAsync(MenuItemCreateDto dto);
        Task<bool> UpdateMenuItemAsync(int id, MenuItemUpdateDto dto);
        Task<bool> DeleteMenuItemAsync(int id);
        Task<bool> ToggleAvailabilityAsync(int id);
    }

    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _repository;

        public MenuItemService(IRepository<MenuItem> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantAsync(int restaurantId)
        {
            return await _repository.FindAsync(m => m.RestaurantId == restaurantId && m.IsAvailable);
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItemCreateDto dto)
        {
            var menuItem = new MenuItem
            {
                RestaurantId = dto.RestaurantId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                IsAvailable = true
            };

            await _repository.AddAsync(menuItem);
            await _repository.SaveChangesAsync();
            return menuItem;
        }

        public async Task<bool> UpdateMenuItemAsync(int id, MenuItemUpdateDto dto)
        {
            var menuItem = await _repository.GetByIdAsync(id);
            if (menuItem == null) return false;

            menuItem.Name = dto.Name;
            menuItem.Description = dto.Description;
            menuItem.Price = dto.Price;
            menuItem.Category = dto.Category;

            _repository.Update(menuItem);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menuItem = await _repository.GetByIdAsync(id);
            if (menuItem == null) return false;

            _repository.Remove(menuItem);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleAvailabilityAsync(int id)
        {
            var menuItem = await _repository.GetByIdAsync(id);
            if (menuItem == null) return false;

            menuItem.IsAvailable = !menuItem.IsAvailable;
            _repository.Update(menuItem);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
