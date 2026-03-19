using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.DTOs.BookingDetail;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.ViewModels.BookingDetail;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingDetailsController : Controller
    {
        private readonly IBookingDetailService _bookingDetailService;
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        public BookingDetailsController(IBookingDetailService bookingDetailService, 
                                        IBookingService bookingService, 
                                        IRoomService roomService)
        {
            _bookingDetailService = bookingDetailService;
            _bookingService = bookingService;
            _roomService = roomService;
        }

        // ===================== INDEX =====================
        public async Task<IActionResult> Index(int bookingID)
        {
            var booking = await _bookingService.GetByIdAsync(bookingID);

            ViewBag.BookingId = bookingID;
            ViewBag.Status = booking.Status;
            ViewBag.BookingCode = booking.BookingCode;
            ViewBag.CustomerName = booking.CustomerName;
            ViewBag.CheckIn = booking.CheckInDate;
            ViewBag.CheckOut = booking.CheckOutDate;

            var details = await _bookingDetailService.GetByBookingIdAsync(bookingID);

            return View(details);
        }

        // ===================== GET BY ID =====================
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _bookingDetailService.GetByIdAsync(id);

            if (detail == null)
                return NotFound();

            return Json(new
            {
                id = detail.Id,
                bookingId = detail.BookingId,
                roomId = detail.RoomId,
                pricePerNight = detail.PricePerNight,
                numberOfNights = detail.NumberOfNights,
                numberOfGuests = detail.NumberOfGuests,
                subTotal = detail.SubTotal
            });
        }

        // ===================== CREATE =====================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDetailVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new BookingDetailCreateDto
            {
                BookingId = vm.BookingId,
                RoomId = vm.RoomId,
                PricePerNight = vm.PricePerNight,
                NumberOfNights = vm.NumberOfNights,
                NumberOfGuests = vm.NumberOfGuests
            };

            var userIdCreate = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _bookingDetailService.CreateAsync(dto, userIdCreate);

            return Json(new { status = 200, message = "Thêm phòng thành công" });
        }

        // ===================== UPDATE =====================
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookingDetailVM vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = 400, message = "Dữ liệu không hợp lệ" });
            }

            var dto = new BookingDetailUpdateDto
            {
                Id = vm.Id,
                RoomId = vm.RoomId,
                PricePerNight = vm.PricePerNight,
                NumberOfNights = vm.NumberOfNights,
                NumberOfGuests = vm.NumberOfGuests
            };

            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

            await _bookingDetailService.UpdateAsync(dto, userId);

            return Json(new { status = 200, message = "Cập nhật thành công" });
        }

        // ===================== DELETE =====================
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var detail = await _bookingDetailService.GetByIdAsync(id);

            if (detail == null)
            {
                return NotFound(new { message = "Không tìm thấy dữ liệu" });
            }

            await _bookingDetailService.DeleteAsync(id);

            return Ok(new { message = "Xóa thành công" });
        }

        // ===================== CHECKOUT =======================
        [HttpPost]
        public async Task<IActionResult> CheckOut(int id)
        {
            try
            {
                var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            );

                await _bookingService.CheckOutAsync(id, userId);

                return Json(new
                {
                    status = 200,
                    message = "CheckOut thành công"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 400,
                    message = ex.Message
                });
            }
        }



    }
}
