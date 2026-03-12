using System.Threading.Tasks;
using FoodOrderingPlatform.Api.Models;
using FoodOrderingPlatform.Api.DTOs;
using FoodOrderingPlatform.Api.Repositories;
using FoodOrderingPlatform.Api.Helpers;

namespace FoodOrderingPlatform.Api.Services
{
    public interface IUserService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtHelper _jwtHelper;

        public UserService(IRepository<User> userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            var token = _jwtHelper.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                UserId = user.UserId
            };
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
                return false;

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "Customer"
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
