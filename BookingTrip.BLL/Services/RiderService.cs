using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Services
{
    public class RiderService : IRiderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFareCalculatorService _fareCalculatorService;
        private readonly IWalletService _walletService;
        private readonly INotificationService _notificationService;

        public RiderService(IUnitOfWork unitOfWork, IFareCalculatorService fareCalculatorService, IWalletService walletService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _fareCalculatorService = fareCalculatorService;
            _walletService = walletService;
            _notificationService = notificationService;
        }

        public async Task<BookingResponseDTO> BookSeatAsync(BookingCreateDTO bookingDto)
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(bookingDto.TripId);
            if (trip == null || trip.AvailableSeats == 0 || trip.Status != TripStatus.Scheduled)
            {
                throw new InvalidOperationException("Trip not available for booking.");
            }

            // Deduct booking fee
            await _walletService.DeductBookingFeeAsync(bookingDto.RiderId, 1m); // 1£ booking fee

            var booking = new Booking
            {
                TripId = bookingDto.TripId,
                RiderId = bookingDto.RiderId,
                PickupLocation = bookingDto.PickupLocation,
                DropoffLocation = bookingDto.DropoffLocation,
                Fare = bookingDto.Fare, // This fare is the remaining fare to be paid in cash
                Status = BookingStatus.Confirmed
            };

            await _unitOfWork.Bookings.AddAsync(booking);
            trip.AvailableSeats--;
            _unitOfWork.Trips.Update(trip);
            await _unitOfWork.CommitAsync();

            await _notificationService.SendNotificationAsync(booking.RiderId, $"Your booking for trip {trip.StartLocation} to {trip.EndLocation} is confirmed.", NotificationType.BookingConfirmed);
            await _notificationService.SendNotificationAsync(trip.DriverId, $"A new booking has been made for your trip from {booking.PickupLocation} to {booking.DropoffLocation}.", NotificationType.BookingConfirmed);

            return new BookingResponseDTO
            {
                Id = booking.Id,
                TripId = booking.TripId,
                RiderId = booking.RiderId,
                PickupLocation = booking.PickupLocation,
                DropoffLocation = booking.DropoffLocation,
                Fare = booking.Fare,
                Status = booking.Status
            };
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetRiderBookingsAsync(int riderId)
        {
            var bookings = await _unitOfWork.Bookings.GetRiderBookingsAsync(riderId);
            return bookings.Select(b => new BookingResponseDTO
            {
                Id = b.Id,
                TripId = b.TripId,
                RiderId = b.RiderId,
                PickupLocation = b.PickupLocation,
                DropoffLocation = b.DropoffLocation,
                Fare = b.Fare,
                Status = b.Status
            });
        }

        public async Task<BookingResponseDTO> RequestMidRoutePickupAsync(int tripId, int riderId, string pickupLocation, string dropoffLocation)
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(tripId);
            if (trip == null)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }

            // This would involve more complex logic to find suitable trips and calculate fare
            // For now, we'll simulate a booking
            var fare = _fareCalculatorService.CalculateFare(trip.StartLocation, trip.EndLocation, isMidRoute: true);

            var booking = new BookingCreateDTO
            {
                TripId = tripId,
                RiderId = riderId,
                PickupLocation = pickupLocation,
                DropoffLocation = dropoffLocation,
                Fare = fare
            };

            // System recommends trip & fare, then driver accepts - this is simplified here
            // In a real system, this would be a multi-step process involving notifications to the driver
            await _notificationService.SendNotificationAsync(trip.DriverId, $"Mid-route pickup request for your trip {trip.Id} from {pickupLocation} to {dropoffLocation}. Suggested fare: {fare:C}.", NotificationType.NearbyRequest);

            return await BookSeatAsync(booking);
        }

        public async Task<RatingResponseDTO> RateDriverAsync(RatingCreateDTO ratingDto)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(ratingDto.BookingId);
            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {ratingDto.BookingId} not found.");
            }

            var rating = new Rating
            {
                BookingId = ratingDto.BookingId,
                Score = ratingDto.Score,
                Feedback = ratingDto.Feedback
            };

            await _unitOfWork.Ratings.AddAsync(rating);
            await _unitOfWork.CommitAsync();

            return new RatingResponseDTO
            {
                Id = rating.Id,
                BookingId = rating.BookingId,
                Score = rating.Score,
                Feedback = rating.Feedback,
                RatingDate = rating.RatingDate
            };
        }
    }
}
