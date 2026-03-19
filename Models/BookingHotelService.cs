using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class BookingHotelService : AuditableEntity
    {
        public int BookingDetailId { get; set; } 

        public BookingDetail BookingDetail { get; set; }

        public int ServiceId { get; set; }
        public HotelService? HotelService { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }
    }

}
