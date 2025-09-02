using BookingTrip.BLL.Interfaces.Services;
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
        // هذا الإجراء يعرض نموذجًا للراكب للبحث عن رحلة.
        public IActionResult SearchTrips()
        {
            return View();
        }

        [HttpPost]
        // هذا الإجراء يتعامل مع طلب البحث عن الرحلات، ويقوم بعرض النتائج.
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
        // هذا الإجراء يتعامل مع طلب حجز مقعد في رحلة معينة.
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
        // هذا الإجراء يعرض قائمة بجميع الحجوزات التي قام بها راكب معين.
        public async Task<IActionResult> MyBookings(int riderId)
        {
            var bookings = await _riderService.GetRiderBookingsAsync(riderId);
            return View(bookings);
        }

        [HttpPost]
        // هذا الإجراء يسمح للراكب بطلب التقاطه من منتصف الطريق في رحلة جارية.
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
        // هذا الإجراء يتعامل مع تقييم الراكب للسائق بعد انتهاء الرحلة.
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
