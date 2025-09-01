using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetRiderBookingsAsync(int riderId);
        Task<IEnumerable<Booking>> GetTripBookingsAsync(int tripId);
    }
}
