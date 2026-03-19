using Project_HotelBooking.Enums;

namespace Project_HotelBooking.ViewModels.Booking
{
    public class BookingVM
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public BookingStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }
        public int RoomCount { get; set; }
    }
}
