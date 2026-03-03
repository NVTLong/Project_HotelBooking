using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class Floor : AuditableEntity
    {
        public string FloorName { get; set; } = null!;
        public int FloorNumber { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
