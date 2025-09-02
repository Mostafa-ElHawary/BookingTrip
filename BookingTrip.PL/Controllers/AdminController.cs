using BookingTrip.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingTrip.PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpGet]
        // هذا الإجراء يعرض لوحة تحكم المسؤول ويجلب قائمة بجميع المستخدمين.
        public async Task<IActionResult> Index()
        {
            var users = await _adminService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        // هذا الإجراء يستخدم لتفعيل أو إلغاء تفعيل حساب سائق.
        public async Task<IActionResult> VerifyDriver(int driverId, bool isVerified)
        {
            try
            {
                await _adminService.VerifyDriverAsync(driverId, isVerified);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        // هذا الإجراء يستخدم لحظر مستخدم معين ومنعه من الوصول إلى النظام.
        public async Task<IActionResult> BlockUser(int userId)
        {
            try
            {
                await _adminService.BlockUserAsync(userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        // هذا الإجراء يستخدم لتعطيل حساب مستخدم معين.
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            try
            {
                await _adminService.DeactivateUserAsync(userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
