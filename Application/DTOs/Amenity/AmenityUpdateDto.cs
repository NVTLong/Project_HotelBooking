namespace Project_HotelBooking.Application.DTOs.Amenity
{
    public class AmenityUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
