using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }

        public string BookingCode { get; set; } = null!;

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public BookingStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }
        public int RoomCount { get; set; }
    }

}
