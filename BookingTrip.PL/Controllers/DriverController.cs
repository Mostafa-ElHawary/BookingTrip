using BookingTrip.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookingTrip.PL.Controllers
{
    public class DriverController : Controller
    {
        private readonly ITripService _tripService;

        public DriverController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public IActionResult PublishTrip()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PublishTrip(TripPublishDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tripDto = new TripCreateDTO
                    {
                        DriverId = model.DriverId,
                        StartLocation = model.StartLocation,
                        EndLocation = model.EndLocation,
                        StartTime = model.StartTime,
                        AvailableSeats = model.AvailableSeats,
                        SuggestedFare = model.SuggestedFare
                    };
                    await _tripService.PublishTripAsync(tripDto);
                    return RedirectToAction(nameof(MyTrips));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyTrips(int driverId)
        {
            var trips = await _tripService.GetDriverTripsAsync(driverId);
            return View(trips);
        }

        [HttpPost]
        public async Task<IActionResult> StartTrip(int tripId)
        {
            try
            {
                await _tripService.StartTripAsync(tripId);
                return RedirectToAction(nameof(MyTrips));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(MyTrips));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AcceptMidRoutePickup(int tripId, string pickupLocation, string dropoffLocation)
        {
            try
            {
                await _tripService.AcceptMidRoutePickupAsync(tripId, pickupLocation, dropoffLocation);
                return RedirectToAction(nameof(MyTrips));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(MyTrips));
            }
        }
    }
}
