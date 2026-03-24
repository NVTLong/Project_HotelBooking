namespace Project_HotelBooking.Application.DTOs.RoomType
{
    public class RoomTypeImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }
    }
}
