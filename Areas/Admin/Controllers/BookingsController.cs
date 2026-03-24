using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.Booking;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;
using Project_HotelBooking.ViewModels.Booking;
using Project_HotelBooking.ViewModels.Room;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IActionResult> Index()
        {
            var booking = await _bookingService.GetAllAsync();
            return View(booking);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var bk = await _bookingService.GetByIdAsync(id);

            if (bk == null)
            {
                return NotFound();
            }
            return Json(new
            {
                id = bk.Id,
                customerId = bk.CustomerId,
                checkInDate = bk.CheckInDate,
                checkOutDate = bk.CheckOutDate,
                totalGuests = bk.TotalGuests,
                note = bk.Note,

            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new BookingCreateDto
            {
                CheckInDate = vm.CheckInDate,
                CheckOutDate = vm.CheckOutDate,
                CustomerId = vm.CustomerId,
                TotalGuests = vm.TotalGuests,
                Note = vm.Note,
                
            };
            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );
            await _bookingService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Tạo phòng thành công" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookingVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new BookingUpdateDto
            {
                Id = vm.Id,
                CustomerId = vm.CustomerId,
                CheckInDate = vm.CheckInDate,
                CheckOutDate = vm.CheckOutDate,
                TotalGuests = vm.TotalGuests,
                Note = vm.Note

            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _bookingService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _bookingService.GetByIdAsync(id);

            if (floor == null)
            {
                return NotFound(new { message = "Không tìm thấy phòng" });
            }

            await _bookingService.DeleteAsync(id);

            return Ok(new { massage = "Xóa thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> CheckIn(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            await _bookingService.CheckInAsync(id, userId);

            return Json(new
            {
                success = true,
                message = "Check-in thành công"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            await _bookingService.ConfirmAsync(id, userId);

            return Json(new
            {
                success = true,
                message = "Xác nhận booking thành công"
            });
        }

        //[HttpPost]
        //public async Task<IActionResult> Cancel(int id)
        //{
        //    var booking = await _bookingService.GetByIdAsync(id);

        //    if (booking == null)
        //        return Json(new { success = false });

        //    booking.Status = BookingStatus.Cancelled;

        //    await _bookingService.UpdateAsync(booking);

        //    return Json(new { success = true });
        //}
        [HttpPost]
        public async Task<IActionResult> CheckOut(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            await _bookingService.CheckOutAsync(id, userId);

            return Json(new
            {
                success = true,
                message = "Đã Check-Out"
            });
        }
    }
}
