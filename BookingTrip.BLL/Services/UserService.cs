using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponseDTO> RegisterUserAsync(UserCreateDTO userDto)
        {
            // Check if username or email already exists
            var existingUser = await _unitOfWork.Users.FindAsync(u => u.UserName == userDto.Username || u.Email == userDto.Email);
            if (existingUser.Any())
            {
                throw new InvalidOperationException("Username or Email already registered.");
            }

            var user = new User
            {
                UserName = userDto.Username,
                Email = userDto.Email,
                Role = userDto.Role,
                IsActive = true
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            // Create a wallet for the new user
            await _unitOfWork.Wallets.AddAsync(new Wallet { UserId = user.Id, Balance = 0m });
            await _unitOfWork.CommitAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<UserResponseDTO> LoginUserAsync(string email, string password)
        {
            var user = (await _unitOfWork.Users.FindAsync(u => u.Email == email)).FirstOrDefault();
            if (user == null) // Authentication will be handled by ASP.NET Core Identity's SignInManager
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return new UserProfileDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<UserResponseDTO> UpdateUserProfileAsync(int userId, UserUpdateDTO userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (userDto.Username != null) user.UserName = userDto.Username;
            if (userDto.Email != null) user.Email = userDto.Email;
            if (userDto.IsActive != null) user.IsActive = userDto.IsActive.Value;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.CommitAsync();

            return new UserResponseDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
    }
}
