using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Promotion;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.ViewModels.Promotion;
using System.Security.Claims;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionsController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionService.GetAllAsync();
            return View(promotions);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var promotion = await _promotionService.GetByIdAsync(id);
            if (promotion == null) return NotFound();

            return Json(new
            {
                id = promotion.Id,
                code = promotion.Code,
                discountAmount = promotion.DiscountAmount,
                isPercentage = promotion.IsPercentage,
                startDate = promotion.StartDate.ToString("yyyy-MM-dd"),
                endDate = promotion.EndDate.ToString("yyyy-MM-dd"),
                isActive = promotion.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PromotionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var dto = new PromotionDto
                {
                    Code = vm.Code,
                    DiscountAmount = vm.DiscountAmount,
                    IsPercentage = vm.IsPercentage,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    IsActive = vm.IsActive
                };

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _promotionService.CreateAsync(dto, userId);

                return Json(new { status = 200, message = "Tạo mã ưu đãi thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] PromotionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var dto = new PromotionUpdateDto
                {
                    Id = vm.Id,
                    Code = vm.Code,
                    DiscountAmount = vm.DiscountAmount,
                    IsPercentage = vm.IsPercentage,
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    IsActive = vm.IsActive
                };

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _promotionService.UpdateAsync(dto, userId);

                return Json(new { status = 200, message = "Cập nhật mã ưu đãi thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _promotionService.DeleteAsync(id);
                return Json(new { status = 200, message = "Xóa mã ưu đãi thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message });
            }
        }
    }
}
