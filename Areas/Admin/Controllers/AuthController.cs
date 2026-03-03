using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Identity;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);

            // ❌ Không cho Customer login ở đây
            if (!roles.Contains("Admin") &&
                !roles.Contains("Receptionist") &&
                !roles.Contains("Accountant"))
            {
                ModelState.AddModelError("", "Bạn không có quyền truy cập");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user, password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("", "Invalid login");
            return View();
        }

    }
}
