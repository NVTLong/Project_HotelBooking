namespace Project_HotelBooking.Application.DTOs.BookingDetail
{
    public class BookingDetailCreateDto
    {
        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public decimal PricePerNight { get; set; }

        public int NumberOfNights { get; set; }

        public int NumberOfGuests { get; set; }
    }

}
