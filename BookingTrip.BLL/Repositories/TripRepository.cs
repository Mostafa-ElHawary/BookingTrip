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
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Trip>> GetAvailableTripsAsync(string startLocation, string endLocation, DateTime startTime)
        {
            return await _dbSet
                .Where(t => t.StartLocation == startLocation &&
                            t.EndLocation == endLocation &&
                            t.StartTime >= startTime &&
                            t.AvailableSeats > 0 &&
                            t.Status == TripStatus.Scheduled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetDriverTripsAsync(int driverId)
        {
            return await _dbSet
                .Where(t => t.DriverId == driverId)
                .ToListAsync();
        }
    }
}
