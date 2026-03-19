using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Service;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.ViewModels.Service;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelServicesController : Controller
    {
        private readonly IHotelServiceService _hotelService;
        public HotelServicesController(IHotelServiceService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IActionResult> Index()
        {
            var amenity = await _hotelService.GetAllAsync();
            return View(amenity);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var amenity = await _hotelService.GetByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = amenity.Id,
                name = amenity.Name,
                price = amenity.Price,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelServiceVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }
            var dto = new HotelServiceCreateDto
            {
                Name = vm.Name,
                Price = vm.Price,
                IsOneTime = vm.IsOneTime,
                Unit = vm.Unit                
            };
            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _hotelService.CreateAsync(dto, userId);

            return Json(new { status = 200, message = "Tạo thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HotelServiceVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new HotelServiceUpdateDto
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Unit = model.Unit,
                IsOneTime = model.IsOneTime
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _hotelService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _hotelService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng" });
            }

            await _hotelService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }
    }
}
