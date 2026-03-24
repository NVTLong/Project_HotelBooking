using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class RoomType : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Room>? Rooms { get; set; } = new List<Room>();
        public ICollection<RoomTypeImage>? RoomTypeImages { get; set; } = new List<RoomTypeImage>();
    }

}
