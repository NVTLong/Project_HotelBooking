namespace Project_HotelBooking.ViewModels.Service
{
    public class HotelServiceVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
        public string Unit { get; set; }
        public bool IsOneTime { get; set; }
        public bool IsActive { get; set; }
    }

}
