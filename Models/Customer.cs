using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{ 
    public class Customer : AuditableEntity
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }

        public string? AvatarUrl { get; set; }

        // Nếu cho login online
        public string? PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }

}
