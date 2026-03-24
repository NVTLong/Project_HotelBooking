namespace Project_HotelBooking.Application.DTOs.Service
{
    public class HotelServiceCreateDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        public string Unit { get; set; }
        public bool IsOneTime { get; set; }
    }

}
