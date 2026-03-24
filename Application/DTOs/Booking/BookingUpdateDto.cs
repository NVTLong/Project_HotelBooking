using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.DTOs.Booking
{
    public class BookingUpdateDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public BookingStatus Status { get; set; }

        public string? Note { get; set; }
    }

}
