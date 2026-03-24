using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class RoomTypeImage : BaseEntity
    {
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
