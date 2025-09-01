using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.BLL.Interfaces
{
    public interface ITripService
    {
        Task<TripResponseDTO> PublishTripAsync(TripCreateDTO tripDto);
        Task<IEnumerable<TripResponseDTO>> SearchTripsAsync(string startLocation, string endLocation, DateTime startTime);
        Task<TripResponseDTO> StartTripAsync(int tripId);
        Task<TripResponseDTO> AcceptMidRoutePickupAsync(int tripId, string pickupLocation, string dropoffLocation);
        Task<IEnumerable<TripResponseDTO>> GetDriverTripsAsync(int driverId);
        Task<TripResponseDTO> UpdateTripAsync(int tripId, TripUpdateDTO tripDto);
    }
}
