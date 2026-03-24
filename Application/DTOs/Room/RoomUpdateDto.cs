using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.DTOs.Room
{
    public class RoomUpdateDto
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int FloorId { get; set; }

        public int RoomTypeId { get; set; }

        public RoomStatus Status { get; set; }
        public List<int>? AmenityIds { get; set; }
        public List<string>? ImageUrls { get; set; }
        public bool IsActive { get; set; }
    }

}
