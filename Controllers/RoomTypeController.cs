using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.Interfaces.Service;

namespace Project_HotelBooking.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var roomType = await _roomTypeService.GetDetailedRoomTypeAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            // Gợi ý các hạng phòng khác
            var allRoomTypes = await _roomTypeService.GetAllAsync();
            ViewBag.OtherRoomTypes = allRoomTypes.Where(rt => rt.Id != id && rt.IsActive).ToList();

            return View(roomType);
        }
    }
}
