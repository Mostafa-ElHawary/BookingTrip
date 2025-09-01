using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTrip.BLL.Interfaces
{
    public interface IFareCalculatorService
    {
        decimal CalculateFare(string startLocation, string endLocation, bool isMidRoute = false);
    }
}
