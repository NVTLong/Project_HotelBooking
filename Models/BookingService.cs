using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class BookingService : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }
    }

}
