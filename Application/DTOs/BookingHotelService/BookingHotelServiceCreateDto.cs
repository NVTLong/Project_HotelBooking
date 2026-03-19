namespace Project_HotelBooking.Application.DTOs.BookingHotelService
{
    public class BookingHotelServiceCreateDto
    {
        public int BookingDetailId { get; set; }

        public int ServiceId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}
