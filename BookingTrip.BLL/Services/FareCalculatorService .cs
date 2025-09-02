using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTrip.BLL.Interfaces.Services;

namespace BookingTrip.BLL.Services
{
    public class FareCalculatorService : IFareCalculatorService
    {
        private const decimal BaseFare = 2.50m;
        private const decimal RatePerKm = 1.20m;
        private const decimal MidRouteSurcharge = 0.50m; // Additional charge per km for mid-route

        public decimal CalculateFare(string startLocation, string endLocation, bool isMidRoute = false)
        {
            // In a real application, this would involve a more sophisticated
            // distance calculation using mapping services (e.g., Google Maps API).
            // For simplicity, we'll use a dummy distance calculation.
            decimal distance = CalculateDistance(startLocation, endLocation);

            decimal fare = BaseFare + (distance * RatePerKm);

            if (isMidRoute)
            {
                fare += (distance * MidRouteSurcharge);
            }

            return fare;
        }

        private decimal CalculateDistance(string startLocation, string endLocation)
        {
            // Dummy distance calculation based on string length difference for demonstration
            // Replace with actual mapping service integration in a real application
            return Math.Abs(startLocation.Length - endLocation.Length) * 1.5m + 5; // Minimum 5km
        }
    }
}
