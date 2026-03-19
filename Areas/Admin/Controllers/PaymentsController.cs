using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentRepository paymentRepository,
                                  IPaymentService paymentService)
        {
            _paymentRepository = paymentRepository;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _paymentRepository.GetAllAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int bookingId, string method)
        {
            try
            {
                await _paymentService.PayAsync(bookingId, method);

                return Json(new { success = true, message = "Thanh toán thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}
