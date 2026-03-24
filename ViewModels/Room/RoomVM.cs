using Project_HotelBooking.Enums;

namespace Project_HotelBooking.ViewModels.Room
{
    public class RoomVM
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int FloorId { get; set; }

        public int RoomTypeId { get; set; }

        public RoomStatus Status { get; set; }

        public bool IsActive { get; set; }

        public string? FloorName { get; set; }

        public string? RoomTypeName { get; set; }
        public List<int>? AmenityIds { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
