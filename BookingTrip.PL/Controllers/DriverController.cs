using BookingTrip.BLL.Interfaces.Services;
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
        // هذا الإجراء يعرض نموذجًا للسائق لنشر رحلة جديدة.
        public IActionResult PublishTrip()
        {
            return View();
        }

        [HttpPost]
        // هذا الإجراء يتعامل مع بيانات نموذج نشر الرحلة، يقوم بإنشاء رحلة جديدة وحفظها في قاعدة البيانات.
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
        // هذا الإجراء يعرض قائمة بجميع الرحلات التي قام بها سائق معين.
        public async Task<IActionResult> MyTrips(int driverId)
        {
            var trips = await _tripService.GetDriverTripsAsync(driverId);
            return View(trips);
        }

        [HttpPost]
        // هذا الإجراء يقوم بتغيير حالة الرحلة إلى "قيد التقدم" (InProgress).
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
        // هذا الإجراء يسمح للسائق بقبول طلب حجز من راكب في منتصف الطريق.
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
