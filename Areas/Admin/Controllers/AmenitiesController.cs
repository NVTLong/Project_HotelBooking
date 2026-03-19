using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Amenity;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.ViewModels.Amenity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AmenitiesController : Controller
    {
        private readonly IAmenityService _amenityService;
        public AmenitiesController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }
        public async Task<IActionResult> Index()
        {
            var amenity = await _amenityService.GetAllAsync();
            return View(amenity);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var amenity = await _amenityService.GetByIdAsync(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = amenity.Id,
                name = amenity.Name,
                description = amenity.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AmenityVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }
            var dto = new AmenityDto
            {
                Name = vm.Name,
                Description = vm.Description,
            };
            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _amenityService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Tạo thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AmenityVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new AmenityUpdateDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _amenityService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _amenityService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng" });
            }

            await _amenityService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAmenities()
        {
            var amenities = await _amenityService.GetAllAsync();

            var result = amenities.Select(x => new
            {
                text = x.Name,
                value = x.Id
            });

            return Ok(new { status = 200, resources = result });
        }
    }
}
