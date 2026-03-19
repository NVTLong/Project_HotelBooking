using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class HotelService : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<BookingHotelService>? BookingServices { get; set; }
    }
}
