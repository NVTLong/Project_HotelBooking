namespace Project_HotelBooking.Application.DTOs.BookingHotelService
{
    public class BookingHotelServiceItemDto
    {
        public int ServiceId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
    public class BookingHotelServiceMultipleDto
    {
        public int BookingDetailId { get; set; }
        public List<BookingHotelServiceItemDto> Items { get; set; }
    }
}
