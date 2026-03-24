using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Areas.Admin.ViewModels;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentRepository _paymentRepository;

        public DashboardController(
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IPaymentRepository paymentRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();
            var bookings = await _bookingRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var payments = await _paymentRepository.GetAllAsync();

            var today = DateTime.Today;
            var last7Days = Enumerable.Range(0, 7).Select(i => today.AddDays(-i)).Reverse().ToList();

            var viewModel = new DashboardVM
            {
                TotalRooms = rooms.Count(),
                RoomsInUse = rooms.Count(r => r.Status == RoomStatus.Occupied),
                TotalBookings = bookings.Count(),
                TotalCustomers = (int)customers.Count(),
                TotalRevenue = payments.Sum(p => p.Amount),
                RecentBookings = bookings.OrderByDescending(b => b.CreatedAt).Take(5).ToList(),
                RoomStatusStats = rooms.GroupBy(r => r.Status)
                                       .ToDictionary(g => g.Key, g => g.Count()),

                // Alerts
                TodayCheckInsCount = bookings.Count(b => b.CheckInDate.Date == today && b.Status != BookingStatus.Cancelled),
                UnpaidBookingsCount = bookings.Count(b => b.Status == BookingStatus.Pending),
                IsLowAvailability = (decimal)rooms.Count(r => r.Status == RoomStatus.Available) / rooms.Count() < 0.15m,

                // Chart Data
                ChartLabels = last7Days.Select(d => d.ToString("dd/MM")).ToList(),
                RevenueData = last7Days.Select(d => payments.Where(p => p.PaidAt >= d.Date && p.PaidAt < d.Date.AddDays(1)).Sum(p => p.Amount)).ToList(),
                BookingData = last7Days.Select(d => bookings.Where(b => b.CreatedAt.Date == d.Date).Count()).ToList()
            };

            return View(viewModel);
        }
    }
}
