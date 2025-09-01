using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;
using BookingTrip.BLL.Repositories;
using BookingTrip.DAL.Data.Context;
using BookingTrip.DAL.Entities;

namespace BookingTrip.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<User> _users;
        private IGenericRepository<Driver> _drivers;
        private IGenericRepository<Rider> _riders;
        private ITripRepository _trips;
        private IBookingRepository _bookings;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Rating> _ratings;
        private IGenericRepository<Notification> _notifications;
        private IGenericRepository<Wallet> _wallets;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Driver> Drivers => _drivers ??= new GenericRepository<Driver>(_context);
        public IGenericRepository<Rider> Riders => _riders ??= new GenericRepository<Rider>(_context);
        public ITripRepository Trips => _trips ??= new TripRepository(_context);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_context);
        public IGenericRepository<Payment> Payments => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Rating> Ratings => _ratings ??= new GenericRepository<Rating>(_context);
        public IGenericRepository<Notification> Notifications => _notifications ??= new GenericRepository<Notification>(_context);
        public IGenericRepository<Wallet> Wallets => _wallets ??= new GenericRepository<Wallet>(_context);

        IGenericRepository<User> IUnitOfWork.Users => throw new NotImplementedException();

        IGenericRepository<Driver> IUnitOfWork.Drivers => throw new NotImplementedException();

        IGenericRepository<Rider> IUnitOfWork.Riders => throw new NotImplementedException();

        IGenericRepository<Payment> IUnitOfWork.Payments => throw new NotImplementedException();

        IGenericRepository<Rating> IUnitOfWork.Ratings => throw new NotImplementedException();

        IGenericRepository<Notification> IUnitOfWork.Notifications => throw new NotImplementedException();

        IGenericRepository<Wallet> IUnitOfWork.Wallets => throw new NotImplementedException();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
