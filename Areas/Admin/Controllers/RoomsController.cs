using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;
using Project_HotelBooking.Enums;
using Project_HotelBooking.Models;
using Project_HotelBooking.ViewModels.Room;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomsController(IRoomService roomService, IRoomTypeService roomTypeService, IWebHostEnvironment webHostEnvironment)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var room = await _roomService.GetAllAsync();
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = room.Id,
                roomNumber = room.RoomNumber,
                roomTypeId = room.RoomTypeId,
                floorId = room.FloorId,
                status = room.Status,
                isActive = room.IsActive,
                amenityIds = room.AmenityIds,
                images = room.RoomImages?.Select(x => x.ImageUrl).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RoomVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var imageUrls = await SaveImages(vm.Images);

            var dto = new RoomCreateDto
            {
                RoomNumber = vm.RoomNumber,
                RoomTypeId = vm.RoomTypeId,
                FloorId = vm.FloorId,
                Status = vm.Status,
                IsActive = vm.IsActive,
                AmenityIds = vm.AmenityIds,
                ImageUrls = imageUrls
            };

            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _roomService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Tạo phòng thành công" });
        }

        [HttpPost] // Use Post for both Create/Update when using FormData
        public async Task<IActionResult> Update([FromForm] RoomVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var imageUrls = await SaveImages(model.Images);

            var dto = new RoomUpdateDto
            {
                Id = model.Id,
                RoomNumber = model.RoomNumber,
                FloorId = model.FloorId,
                RoomTypeId = model.RoomTypeId,
                Status = model.Status,
                IsActive = model.IsActive,
                AmenityIds = model.AmenityIds,
                ImageUrls = imageUrls.Any() ? imageUrls : null
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _roomService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        private async Task<List<string>> SaveImages(List<IFormFile>? images)
        {
            var urls = new List<string>();
            if (images == null || !images.Any()) return urls;

            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "rooms");
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    urls.Add("/uploads/rooms/" + fileName);
                }
            }
            return urls;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _roomService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng" });
            }

            await _roomService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRoomsByType(int roomTypeId)
        {
            var rooms = await _roomService.GetAllAsync();

            var data = rooms
                .Where(x => x.RoomTypeId == roomTypeId
                         && x.Status == RoomStatus.Available
                         && x.IsActive)
                .Select(x => new
                {
                    value = x.Id,
                    text = x.RoomNumber,
                    price = x.Price
                });

            return Json(new
            {
                status = 200,
                resources = data
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);

            if (room == null)
                return NotFound();

            return Json(new
            {
                status = 200,
                data = room
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomTypes()
        {
            var roomTypes = await _roomTypeService.GetAllAsync();

            var data = roomTypes.Select(x => new
            {
                value = x.Id,
                text = x.Name
            });

            return Json(new
            {
                status = 200,
                resources = data
            });
        }

    }
}
