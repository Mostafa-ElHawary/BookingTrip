using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.PL.Dtos;

namespace BookingTrip.BLL.Interfaces.Services
{
    public interface IRiderService
    {
        Task<BookingResponseDTO> BookSeatAsync(BookingCreateDTO bookingDto);
        Task<IEnumerable<BookingResponseDTO>> GetRiderBookingsAsync(int riderId);
        Task<BookingResponseDTO> RequestMidRoutePickupAsync(int tripId, int riderId, string pickupLocation, string dropoffLocation);
        Task<RatingResponseDTO> RateDriverAsync(RatingCreateDTO ratingDto);
    }
}
