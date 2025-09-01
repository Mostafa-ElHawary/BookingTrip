using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<DriverResponseDTO> VerifyDriverAsync(int driverId, bool isVerified);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO> BlockUserAsync(int userId);
        Task<UserResponseDTO> DeactivateUserAsync(int userId);
    }
}
