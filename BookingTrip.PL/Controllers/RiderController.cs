using BookingTrip.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookingTrip.PL.Controllers
{
    public class RiderController : Controller
    {
        private readonly IRiderService _riderService;
        private readonly ITripService _tripService;

        public RiderController(IRiderService riderService, ITripService tripService)
        {
            _riderService = riderService;
            _tripService = tripService;
        }

        [HttpGet]
        public IActionResult SearchTrips()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchTrips(TripSearchDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trips = await _tripService.SearchTripsAsync(model.StartLocation, model.EndLocation, model.StartTime);
                    return View("TripSearchResults", trips);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookTrip(BookingCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _riderService.BookSeatAsync(model);
                    return RedirectToAction(nameof(MyBookings));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings(int riderId)
        {
            var bookings = await _riderService.GetRiderBookingsAsync(riderId);
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> RequestMidRoutePickup(int tripId, int riderId, string pickupLocation, string dropoffLocation)
        {
            try
            {
                await _riderService.RequestMidRoutePickupAsync(tripId, riderId, pickupLocation, dropoffLocation);
                return RedirectToAction(nameof(MyBookings));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(MyBookings));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RateDriver(RatingCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _riderService.RateDriverAsync(model);
                    return RedirectToAction(nameof(MyBookings));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
    }
}
