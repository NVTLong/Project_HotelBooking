namespace Project_HotelBooking.Application.DTOs.Room
{
    public class RoomImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
