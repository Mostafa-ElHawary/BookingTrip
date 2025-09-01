using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;
using BookingTrip.DAL.Data.Context;
using BookingTrip.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTrip.BLL.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Booking>> GetRiderBookingsAsync(int riderId)
        {
            return await _dbSet
                .Where(b => b.RiderId == riderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetTripBookingsAsync(int tripId)
        {
            return await _dbSet
                .Where(b => b.TripId == tripId)
                .ToListAsync();
        }
    }
}
