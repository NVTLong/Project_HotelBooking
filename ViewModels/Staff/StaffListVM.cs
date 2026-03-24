using Project_HotelBooking.Enums;

namespace Project_HotelBooking.ViewModels.Staff
{
    public class StaffListVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string? EmployeeCode { get; set; }

        public UserRole Role { get; set; }
    }
}
