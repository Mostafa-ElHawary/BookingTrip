using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.BLL.Interfaces
{
    public interface IWalletService
    {
        Task DeductBookingFeeAsync(int userId, decimal amount);
        Task<decimal> GetUserBalanceAsync(int userId);
    }
}
