using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.ViewModels.Staff
{
    public class StaffVM
    {
        public int? Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public string? EmployeeCode { get; set; }
        public DateTime? StartWorkingDate { get; set; }

        [Required]
        public string RoleName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
