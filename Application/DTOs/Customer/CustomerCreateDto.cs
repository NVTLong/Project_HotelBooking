namespace Project_HotelBooking.Application.DTOs.Customer
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? IdentityNumber { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Password { get; set; }
    }
}
