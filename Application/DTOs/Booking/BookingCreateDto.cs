namespace Project_HotelBooking.Application.DTOs.Booking
{
    public class BookingCreateDto
    {
        public int CustomerId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public string? Note { get; set; }
    }
}
