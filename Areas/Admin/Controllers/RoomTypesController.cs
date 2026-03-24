using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.RoomType;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.ViewModels.RoomType;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var roomType = await _roomTypeService.GetAllAsync();
            return View(roomType);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var roomType = await _roomTypeService.GetByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = roomType.Id,
                name = roomType.Name,
                description = roomType.Description,
                basePrice = roomType.BasePrice,
                capacity = roomType.Capacity,
                isActive = roomType.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomTypeVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new RoomTypeCreateDto
            {
                Name = vm.Name,
                Capacity = vm.Capacity,
                Description = vm.Description,
                BasePrice = vm.BasePrice,
            };
            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _roomTypeService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Tạo tầng thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomTypeVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new RoomTypeUpdateDto
            {
                Id = model.Id,
                Name = model.Name,
                Capacity = model.Capacity,
                Description = model.Description,
                BasePrice = model.BasePrice,
                IsActive = model.IsActive
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _roomTypeService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _roomTypeService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy tầng" });
            }

            await _roomTypeService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }


        [HttpGet]
        public async Task<IActionResult> GetRoomTypes()
        {
            var data = await _roomTypeService.GetAllAsync();

            var result = data.Select(x => new
            {
                text = x.Name,
                value = x.Id
            });

            return Ok(new { status = 200, resources = result });
        }
    }
}
