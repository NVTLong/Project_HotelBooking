using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class Amenity : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}
