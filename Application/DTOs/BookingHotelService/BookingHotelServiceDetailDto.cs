namespace Project_HotelBooking.Application.DTOs.BookingHotelService
{
    public class BookingHotelServiceDetailDto
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }

}
