using Microsoft.IdentityModel.Tokens;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Application.DTOs
{
    public class StaffUpsertDto
    {
        public string? Id { get; set; }

        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? EmployeeCode { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? StartWorkingDate { get; set; }

        public UserRole Role { get; set; }

        public bool MustChangPassword { get; set; } = false; // Bắt buộc staff đổi mật khẩu

        public bool IsActive { get; set; } = true;

        public bool IsEdit => !string.IsNullOrEmpty(Id);
    }
}
