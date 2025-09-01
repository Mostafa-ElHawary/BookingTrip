using BookingTrip.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookingTrip.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userDto = new UserCreateDTO
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password,
                        Role = model.Role
                    };
                    var user = await _userService.RegisterUserAsync(userDto);
                    // Optionally sign in the user after registration
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.LoginUserAsync(model.Email, model.Password);
                    // Implement ASP.NET Core Identity sign-in here
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Implement ASP.NET Core Identity sign-out here
            return RedirectToAction("Login");
        }
    }
}
