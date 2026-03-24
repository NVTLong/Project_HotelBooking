using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.DTOs.Room
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int FloorId { get; set; }
        public string? FloorName { get; set; }

        public int RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public decimal Price { get; set; }  
        public RoomStatus Status { get; set; }
        public List<int>? AmenityIds { get; set; }
        public List<RoomImageDto>? RoomImages { get; set; }
        public bool IsActive { get; set; }
    }
}
