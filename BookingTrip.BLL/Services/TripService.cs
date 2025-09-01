using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TripService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TripResponseDTO> PublishTripAsync(TripCreateDTO tripDto)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(tripDto.DriverId);
            if (driver == null)
            {
                throw new KeyNotFoundException($"Driver with ID {tripDto.DriverId} not found.");
            }

            var trip = new Trip
            {
                DriverId = tripDto.DriverId,
                StartLocation = tripDto.StartLocation,
                EndLocation = tripDto.EndLocation,
                StartTime = tripDto.StartTime,
                AvailableSeats = tripDto.AvailableSeats,
                SuggestedFare = tripDto.SuggestedFare,
                Status = TripStatus.Scheduled
            };

            await _unitOfWork.Trips.AddAsync(trip);
            await _unitOfWork.CommitAsync();

            // Notify riders about new trip (this would involve a notification service)
            // For now, we'll just log it.
            Console.WriteLine($"New trip published from {trip.StartLocation} to {trip.EndLocation} by Driver {trip.DriverId}");

            return new TripResponseDTO
            {
                Id = trip.Id,
                DriverId = trip.DriverId,
                StartLocation = trip.StartLocation,
                EndLocation = trip.EndLocation,
                StartTime = trip.StartTime,
                AvailableSeats = trip.AvailableSeats,
                SuggestedFare = trip.SuggestedFare,
                Status = trip.Status
            };
        }

        public async Task<IEnumerable<TripResponseDTO>> SearchTripsAsync(string startLocation, string endLocation, DateTime startTime)
        {
            var trips = await _unitOfWork.Trips.GetAvailableTripsAsync(startLocation, endLocation, startTime);
            return trips.Select(t => new TripResponseDTO
            {
                Id = t.Id,
                DriverId = t.DriverId,
                StartLocation = t.StartLocation,
                EndLocation = t.EndLocation,
                StartTime = t.StartTime,
                AvailableSeats = t.AvailableSeats,
                SuggestedFare = t.SuggestedFare,
                Status = t.Status
            });
        }

        public async Task<TripResponseDTO> StartTripAsync(int tripId)
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }

            trip.Status = TripStatus.InProgress;
            _unitOfWork.Trips.Update(trip);
            await _unitOfWork.CommitAsync();

            // Notify riders that trip has started
            Console.WriteLine($"Trip {trip.Id} from {trip.StartLocation} to {trip.EndLocation} has started.");

            return new TripResponseDTO
            {
                Id = trip.Id,
                DriverId = trip.DriverId,
                StartLocation = trip.StartLocation,
                EndLocation = trip.EndLocation,
                StartTime = trip.StartTime,
                AvailableSeats = trip.AvailableSeats,
                SuggestedFare = trip.SuggestedFare,
                Status = trip.Status
            };
        }

        public async Task<TripResponseDTO> AcceptMidRoutePickupAsync(int tripId, string pickupLocation, string dropoffLocation)
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }

            // Logic to calculate proportional fare for mid-route pickup and update trip details
            // This would typically involve a FareCalculatorService
            Console.WriteLine($"Driver accepted mid-route pickup for Trip {trip.Id} from {pickupLocation} to {dropoffLocation}");

            return new TripResponseDTO
            {
                Id = trip.Id,
                DriverId = trip.DriverId,
                StartLocation = trip.StartLocation,
                EndLocation = trip.EndLocation,
                StartTime = trip.StartTime,
                AvailableSeats = trip.AvailableSeats,
                SuggestedFare = trip.SuggestedFare,
                Status = trip.Status
            };
        }

        public async Task<IEnumerable<TripResponseDTO>> GetDriverTripsAsync(int driverId)
        {
            var trips = await _unitOfWork.Trips.GetDriverTripsAsync(driverId);
            return trips.Select(t => new TripResponseDTO
            {
                Id = t.Id,
                DriverId = t.DriverId,
                StartLocation = t.StartLocation,
                EndLocation = t.EndLocation,
                StartTime = t.StartTime,
                AvailableSeats = t.AvailableSeats,
                SuggestedFare = t.SuggestedFare,
                Status = t.Status
            });
        }

        public async Task<TripResponseDTO> UpdateTripAsync(int tripId, TripUpdateDTO tripDto)
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }

            if (tripDto.StartLocation != null) trip.StartLocation = tripDto.StartLocation;
            if (tripDto.EndLocation != null) trip.EndLocation = tripDto.EndLocation;
            if (tripDto.StartTime != null) trip.StartTime = tripDto.StartTime.Value;
            if (tripDto.AvailableSeats != null) trip.AvailableSeats = tripDto.AvailableSeats.Value;
            if (tripDto.SuggestedFare != null) trip.SuggestedFare = tripDto.SuggestedFare.Value;
            if (tripDto.Status != null) trip.Status = tripDto.Status.Value;

            _unitOfWork.Trips.Update(trip);
            await _unitOfWork.CommitAsync();

            return new TripResponseDTO
            {
                Id = trip.Id,
                DriverId = trip.DriverId,
                StartLocation = trip.StartLocation,
                EndLocation = trip.EndLocation,
                StartTime = trip.StartTime,
                AvailableSeats = trip.AvailableSeats,
                SuggestedFare = trip.SuggestedFare,
                Status = trip.Status
            };
        }
    }
}
