using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.DTOs;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Identity;
using Project_HotelBooking.ViewModels.Staff;
using System.Security.Claims;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly UserManager<AppUser> _userManager;

        public StaffController(IStaffService staffService, UserManager<AppUser> userManager)
        {
            _staffService = staffService;
            _userManager = userManager;
        }

        // =======================
        // GET: /Staff
        // =======================
        public async Task<IActionResult> Index()
        {
            var vm = new StaffPageVM
            {
                Staffs = await _staffService.GetAllAsync(),
                StaffForm = new StaffUpsertDto()
            };

            return View(vm);
        }

        // =======================
        // CREATE + UPDATE
        // =======================
        [HttpPost]
        public async Task<IActionResult> SaveStaff(StaffUpsertDto model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Dữ liệu không hợp lệ"
                });
            }

            var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _staffService.UpsertAsync(model, adminId);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                tempPassword = result.TempPassword,
                username = result.Username
            });
        }

        // ==========================
        // Lấy thông tin nhân viên để edit
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetStaff(int id)
        {
            var staff = await _staffService.GetByIdAsync(id);

            if (staff == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không tìm thấy nhân viên"
                });
            }

            return Json(new
            {
                success = true,
                data = staff
            });
        }

        // ===========================
        // Xóa nhân viên
        // ===========================
        [HttpPost]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không tìm thấy nhân viên"
                });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = string.Join(",", result.Errors.Select(x => x.Description))
                });
            }

            return Json(new
            {
                success = true,
                message = "Xóa nhân viên thành công"
            });
        }

        // ========================
        // Reset mật khẩu khi nhân viên quên mật khẩu
        // ========================
        [HttpPost]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Không tìm thấy nhân viên"
                });
            }

            var newPassword = "Emp@" + Guid.NewGuid().ToString("N").Substring(0, 8);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return Json(new
                {
                    success = false,
                    message = string.Join(",", result.Errors.Select(x => x.Description))
                });
            }

            user.MustChangePassword = true;
            await _userManager.UpdateAsync(user);

            return Json(new
            {
                success = true,
                username = user.UserName,
                password = newPassword
            });
        }

    }
}
