using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.PL.Dtos;

namespace BookingTrip.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterUserAsync(UserCreateDTO userDto);
        Task<UserResponseDTO> LoginUserAsync(string email, string password);
        Task<UserProfileDTO> GetUserProfileAsync(int userId);
        Task<UserResponseDTO> UpdateUserProfileAsync(int userId, UserUpdateDTO userDto);
    }
}
