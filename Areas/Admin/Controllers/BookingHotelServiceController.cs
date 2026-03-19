using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.BookingHotelService;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingHotelServiceController : Controller
    {
        private readonly IBookingHotelServiceService _bookingHotelServiceService;
        private readonly IHotelServiceService _hotelServiceService;
        public BookingHotelServiceController(IBookingHotelServiceService service, IHotelServiceService hotelServiceService)
        {
            _bookingHotelServiceService = service;
            _hotelServiceService = hotelServiceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _hotelServiceService.GetAllAsync();

            var data = services.Select(x => new
            {
                value = x.Id,
                text = x.Name,
                price = x.Price,
                unit = x.Unit,
                isOneTime = x.IsOneTime
            });

            return Json(new
            {
                status = 200,
                resources = data
            });
        }


        [HttpPost]
        public async Task<IActionResult> CreateMultiple([FromBody] BookingHotelServiceMultipleDto dto, int userId)
        {
            await _bookingHotelServiceService.AddMultipleAsync(dto, userId);

            return Json(new { status = 200 });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookingHotelServiceService.RemoveServiceAsync(id);

            return Json(new { success = true });
        }
    }
}
