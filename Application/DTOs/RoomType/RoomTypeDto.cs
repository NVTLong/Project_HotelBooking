namespace Project_HotelBooking.Application.DTOs.RoomType
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

}
