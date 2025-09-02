using BookingTrip.BLL.Interfaces.Services;
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
        // هذا الإجراء يعرض صفحة التسجيل للمستخدم
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // هذا الإجراء يتعامل مع بيانات نموذج التسجيل، يقوم بإنشاء مستخدم جديد ويحفظه في قاعدة البيانات.
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
                    // يمكنك اختيار تسجيل دخول المستخدم تلقائيًا بعد التسجيل
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
        // هذا الإجراء يعرض صفحة تسجيل الدخول للمستخدم
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        // هذا الإجراء يتعامل مع بيانات نموذج تسجيل الدخول، يقوم بالتحقق من صحة بيانات المستخدم وتسجيل دخوله.
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.LoginUserAsync(model.Email, model.Password);
                    // هنا يتم تنفيذ عملية تسجيل الدخول باستخدام ASP.NET Core Identity
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
        // هذا الإجراء يقوم بتسجيل خروج المستخدم من النظام.
        public async Task<IActionResult> Logout()
        {
            // هنا يتم تنفيذ عملية تسجيل الخروج باستخدام ASP.NET Core Identity
            return RedirectToAction("Login");
        }
    }
}
