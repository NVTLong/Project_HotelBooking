using Project_HotelBooking.Application.DTOs;

namespace Project_HotelBooking.ViewModels.Staff
{
    public class StaffPageVM
    {
        public List<StaffListVM> Staffs { get; set; } = new();

        public StaffUpsertDto StaffForm { get; set; } = new();
    }
}
