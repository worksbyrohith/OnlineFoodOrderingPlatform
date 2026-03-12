using System.Collections.Generic;
using System.Threading.Tasks;
using FoodOrderingPlatform.Api.Models;
using FoodOrderingPlatform.Api.Repositories;
using FoodOrderingPlatform.Api.DTOs;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync();
        Task<Restaurant?> GetRestaurantByIdAsync(int id);
        Task<Restaurant> CreateRestaurantAsync(RestaurantCreateDto dto);
        Task<bool> UpdateRestaurantAsync(int id, RestaurantUpdateDto dto);
        Task<bool> SoftDeleteRestaurantAsync(int id);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> _repository;

        public RestaurantService(IRepository<Restaurant> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurant>> GetActiveRestaurantsAsync()
        {
            return await _repository.FindAsync(r => r.IsActive);
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Restaurant> CreateRestaurantAsync(RestaurantCreateDto dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                ImageUrl = dto.ImageUrl,
                IsActive = true
            };

            await _repository.AddAsync(restaurant);
            await _repository.SaveChangesAsync();
            return restaurant;
        }

        public async Task<bool> UpdateRestaurantAsync(int id, RestaurantUpdateDto dto)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return false;

            restaurant.Name = dto.Name;
            restaurant.Address = dto.Address;
            restaurant.Phone = dto.Phone;
            restaurant.ImageUrl = dto.ImageUrl;
            restaurant.IsActive = dto.IsActive;

            _repository.Update(restaurant);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteRestaurantAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            if (restaurant == null) return false;

            restaurant.IsActive = false;
            _repository.Update(restaurant);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
