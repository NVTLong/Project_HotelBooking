using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.DTOs.RoomType;

namespace Project_HotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomService _roomService;

        public RoomController(IRoomTypeService roomTypeService, IRoomService roomService)
        {
            _roomTypeService = roomTypeService;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? checkIn, DateTime? checkOut, int adults = 1, int children = 0)
        {
            // Mặc định là ngày mai nếu không chọn
            var start = checkIn ?? DateTime.Today.AddDays(1);
            var end = checkOut ?? start.AddDays(1);
            var totalGuests = adults + children;

            ViewData["CheckIn"] = start.ToString("yyyy-MM-dd");
            ViewData["CheckOut"] = end.ToString("yyyy-MM-dd");
            ViewData["Adults"] = adults;
            ViewData["Children"] = children;

            var rooms = await _roomService.GetAvailableRoomsAsync(start, end, totalGuests);
            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null) return NotFound();

            var samePrice = await _roomService.GetRoomsWithSamePriceAsync(id);
            var otherTypes = await _roomService.GetRoomsOfOtherTypesAsync(id);

            ViewBag.SamePriceRooms = samePrice;
            ViewBag.OtherTypeRooms = otherTypes;

            return View(room);
        }
    }
}
