namespace Project_HotelBooking.Application.DTOs.Service
{
    public class HotelServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        public string Unit { get; set; } // 👈 kg, cái, ngày, lần

        public bool IsOneTime { get; set; } // 👈 true = chỉ 1 lần
        public bool IsActive { get; set; }
    }
}
