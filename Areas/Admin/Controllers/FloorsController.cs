using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Floor;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;
using Project_HotelBooking.ViewModels.Floor;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FloorsController : Controller
    {
        private readonly IFloorService _floorService;
        public FloorsController(IFloorService floorService)
        {
            _floorService = floorService;
        }
        public async Task<IActionResult> Index()
        {
            var floor = await _floorService.GetAllAsync();
            return View(floor);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);

            if (floor == null)
                return NotFound();

            return Json(new
            {
                id = floor.Id,
                floorName = floor.FloorName,
                floorNumber = floor.FloorNumber
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FloorVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new CreateFloorDto
            {
                FloorName = vm.FloorName,
                FloorNumber = vm.FloorNumber
            };

            await _floorService.CreateAsync(dto);

            return Json(new { status = 200, message = "Tạo tầng thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FloorVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new UpdateFloorDto
            {
                Id = model.Id,
                FloorName = model.FloorName,
                FloorNumber = model.FloorNumber
            };

            await _floorService.UpdateAsync(dto);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _floorService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new {message = "Không tìm thấy tầng"});
            }

            await _floorService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }


        [HttpGet]
        public async Task<IActionResult> GetFloors()
        {
            var data = await _floorService.GetAllAsync();

            var result = data.Select(x => new
            {
                text = x.FloorName,
                value = x.Id
            });

            return Ok(new { status = 200, resources = result });
        }
    }
}
