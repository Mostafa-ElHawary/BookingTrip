using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces;

namespace BookingTrip.BLL.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeductBookingFeeAsync(int userId, decimal amount)
        {
            var wallet = await _unitOfWork.Wallets.FindAsync(w => w.UserId == userId);
            var userWallet = wallet.FirstOrDefault();

            if (userWallet == null)
            {
                throw new KeyNotFoundException($"Wallet not found for user ID {userId}.");
            }

            if (userWallet.Balance < amount)
            {
                throw new InvalidOperationException($"Insufficient balance in wallet for user ID {userId}.");
            }

            userWallet.Balance -= amount;
            _unitOfWork.Wallets.Update(userWallet);
            await _unitOfWork.CommitAsync();
        }

        public async Task<decimal> GetUserBalanceAsync(int userId)
        {
            var wallet = await _unitOfWork.Wallets.FindAsync(w => w.UserId == userId);
            var userWallet = wallet.FirstOrDefault();

            if (userWallet == null)
            {
                throw new KeyNotFoundException($"Wallet not found for user ID {userId}.");
            }

            return userWallet.Balance;

        }
