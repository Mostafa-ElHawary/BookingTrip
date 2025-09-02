using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.PL.Dtos;

namespace BookingTrip.BLL.Interfaces.Services
{
    public interface IAdminService
    {
        Task<DriverResponseDTO> VerifyDriverAsync(int driverId, bool isVerified);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO> BlockUserAsync(int userId);
        Task<UserResponseDTO> DeactivateUserAsync(int userId);
    }
}
