using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class Service : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<BookingService>? BookingServices { get; set; }
    }
}
