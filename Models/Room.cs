using Project_HotelBooking.Base;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Models
{
    public class Room : AuditableEntity
    {
        public string RoomNumber { get; set; } = null!;

        public int FloorId { get; set; }
        public Floor? Floor { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        public RoomStatus Status { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<RoomImage>? RoomImages { get; set; }
        public ICollection<BookingDetail>? BookingDetails { get; set; }
        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}
