namespace Project_HotelBooking.ViewModels.RoomType
{
    public class RoomTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
