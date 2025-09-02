using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Interfaces.Repositories
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetAvailableTripsAsync(string startLocation, string endLocation, DateTime startTime);
        Task<IEnumerable<Trip>> GetDriverTripsAsync(int driverId);
    }
}
