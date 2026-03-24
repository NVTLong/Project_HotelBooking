using Project_HotelBooking.Application.DTOs.BookingHotelService;

namespace Project_HotelBooking.Application.DTOs.BookingDetail
{
    public class BookingDetailDto
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public decimal PricePerNight { get; set; }

        public int NumberOfNights { get; set; }

        public int NumberOfGuests { get; set; }

        public decimal SubTotal { get; set; }
        public List<BookingHotelServiceDetailDto> HotelServices { get; set; }

    }

}
