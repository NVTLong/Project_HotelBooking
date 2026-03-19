namespace Project_HotelBooking.ViewModels.Customer
{
    public class CustomerVM
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? IdentityNumber { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
