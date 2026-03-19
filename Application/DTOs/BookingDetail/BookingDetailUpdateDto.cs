namespace Project_HotelBooking.Application.DTOs.BookingDetail
{
    public class BookingDetailUpdateDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public decimal PricePerNight { get; set; }

        public int NumberOfNights { get; set; }

        public int NumberOfGuests { get; set; }
    }

}
