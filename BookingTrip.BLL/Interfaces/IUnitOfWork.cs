using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces.Repositories;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Driver> Drivers { get; }
        IGenericRepository<Rider> Riders { get; }
        ITripRepository Trips { get; }
        IBookingRepository Bookings { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Rating> Ratings { get; }
        IGenericRepository<Notification> Notifications { get; }
        IGenericRepository<Wallet> Wallets { get; }

        Task<int> CommitAsync();
    }
}
