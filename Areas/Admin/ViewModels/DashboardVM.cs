using Project_HotelBooking.Models;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Areas.Admin.ViewModels
{
    public class DashboardVM
    {
        public int TotalRooms { get; set; }
        public int RoomsInUse { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalCustomers { get; set; }
        public List<Booking>? RecentBookings { get; set; }
        public Dictionary<RoomStatus, int>? RoomStatusStats { get; set; }

        // Alerts
        public int TodayCheckInsCount { get; set; }
        public int UnpaidBookingsCount { get; set; }
        public bool IsLowAvailability { get; set; }

        // Chart Data (Last 7 Days)
        public List<string>? ChartLabels { get; set; }
        public List<decimal>? RevenueData { get; set; }
        public List<int>? BookingData { get; set; }
    }
}
