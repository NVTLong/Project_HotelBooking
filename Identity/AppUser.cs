using Microsoft.AspNetCore.Identity;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? FullName { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; }
        public int? CreatedByUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedByUserId { get; set; }

        public string? EmployeeCode { get; set; }      // Mã nhân viên
        public string? Address { get; set; }          // Địa chỉ
        public DateTime? DateOfBirth { get; set; }    // Ngày sinh
        public DateTime? StartWorkingDate { get; set; } // Ngày vào làm
        public bool IsActive { get; set; } = true;    // Còn làm việc?
        public string? AvatarUrl { get; set; }        // Ảnh đại diện
        public UserRole Role { get; set; }

        public bool MustChangePassword { get; set; } = false; // Bắt buộc staff đổi mật khẩu

        public bool IsDeleted { get; set; }

    }

}
