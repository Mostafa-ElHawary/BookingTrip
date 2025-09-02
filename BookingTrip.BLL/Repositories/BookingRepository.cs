using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces.Repositories;
using BookingTrip.DAL.Data.Context;
using BookingTrip.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTrip.BLL.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context) { }


        // جلب جميع الحجوزات الخاصة براكب معين
        public async Task<IEnumerable<Booking>> GetRiderBookingsAsync(int riderId)
        {
            return await _dbSet
                .Where(b => b.RiderId == riderId)
                .ToListAsync();
        }
        // يقوم بجلب جميع الحجوزات الخاصة برحلة معينة
        public async Task<IEnumerable<Booking>> GetTripBookingsAsync(int tripId)
        {
            return await _dbSet
                .Where(b => b.TripId == tripId)
                .ToListAsync();
        }
    }
}
