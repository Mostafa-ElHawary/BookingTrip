using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;

namespace BookingTrip.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DriverResponseDTO> VerifyDriverAsync(int driverId, bool isVerified)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(driverId);
            if (driver == null)
            {
                throw new KeyNotFoundException($"Driver with ID {driverId} not found.");
            }

            driver.IsVerified = isVerified;
            _unitOfWork.Drivers.Update(driver);
            await _unitOfWork.CommitAsync();

            return new DriverResponseDTO
            {
                Id = driver.Id,
                UserId = driver.UserId,
                LicenseNumber = driver.LicenseNumber,
                CarInfo = driver.CarInfo,
                IsVerified = driver.IsVerified
            };
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive
            });
        }

        public async Task<UserResponseDTO> BlockUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            user.IsActive = false;
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

        public async Task<UserResponseDTO> DeactivateUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            user.IsActive = false;
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
