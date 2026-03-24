using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Customer;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models.ViewModels;

namespace Project_HotelBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountController(ICustomerService customerService, IBookingService bookingService, IWebHostEnvironment webHostEnvironment)
        {
            _customerService = customerService;
            _bookingService = bookingService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.Session.GetString("CustomerEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null) return RedirectToAction("Logout");

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(CustomerDto model, IFormFile? AvatarFile)
        {
            var email = HttpContext.Session.GetString("CustomerEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _customerService.GetByEmailAsync(email);
                    if (customer == null) return RedirectToAction("Logout");

                    // Xử lý upload Avatar
                    if (AvatarFile != null && AvatarFile.Length > 0)
                    {
                        var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
                        if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(customer.AvatarUrl))
                        {
                            var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, customer.AvatarUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                        }

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(AvatarFile.FileName)}";
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await AvatarFile.CopyToAsync(stream);
                        }

                        model.AvatarUrl = $"/uploads/avatars/{fileName}";
                    }
                    else
                    {
                        model.AvatarUrl = customer.AvatarUrl; // Giữ lại ảnh cũ
                    }

                    await _customerService.UpdateAsync(new CustomerUpdateDto
                    {
                        Id = model.Id,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        IdentityNumber = model.IdentityNumber,
                        AvatarUrl = model.AvatarUrl
                    }, model.Id);

                    // Cập nhật lại Session Name nếu đổi FullName
                    HttpContext.Session.SetString("CustomerName", model.FullName);
                    if (!string.IsNullOrEmpty(model.AvatarUrl))
                    {
                        HttpContext.Session.SetString("CustomerAvatar", model.AvatarUrl);
                    }

                    TempData["Success"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("Profile");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Profile", model);
        }

        [HttpGet]
        public async Task<IActionResult> BookingHistory()
        {
            var email = HttpContext.Session.GetString("CustomerEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            var customer = await _customerService.GetByEmailAsync(email);
            if (customer == null) return RedirectToAction("Logout");

            var bookings = await _bookingService.GetByCustomerIdAsync(customer.Id);
            return View(bookings);
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null, string? mode = "login")
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new AuthViewModel { ActiveMode = mode ?? "login" };
            return View("Authenticate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel model, string? returnUrl = null)
        {
            ModelState.Clear();

            if (TryValidateModel(model.Login, nameof(model.Login)))
            {
                try
                {
                    var result = await _customerService.LoginAsync(new CustomerLoginDto
                    {
                        Email = model.Login.Email,
                        Password = model.Login.Password
                    });

                    HttpContext.Session.SetString("CustomerEmail", result.Email);
                    HttpContext.Session.SetString("CustomerName", result.FullName);
                    if (!string.IsNullOrEmpty(result.AvatarUrl))
                    {
                        HttpContext.Session.SetString("CustomerAvatar", result.AvatarUrl);
                    }

                    return Redirect(returnUrl ?? "/");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            model.ActiveMode = "login";
            return View("Authenticate", model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Login", new { mode = "register" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthViewModel model)
        {
            ModelState.Clear();

            if (TryValidateModel(model.Register, nameof(model.Register)))
            {
                try
                {
                    await _customerService.RegisterAsync(new CustomerRegisterDto
                    {
                        FullName = model.Register.FullName,
                        Email = model.Register.Email,
                        PhoneNumber = model.Register.PhoneNumber,
                        Password = model.Register.Password
                    });

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            model.ActiveMode = "register";
            return View("Authenticate", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
