using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class RoomImage : BaseEntity
    {
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }

}
