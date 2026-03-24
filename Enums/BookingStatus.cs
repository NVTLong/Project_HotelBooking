using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.Enums
{
    public enum BookingStatus
    {
        [Display(Name = "Chờ xử lý")]
        Pending = 1,

        [Display(Name = "Đã xác nhận")]
        Confirmed = 2,

        [Display(Name = "Đã nhận phòng")]
        CheckedIn = 3,

        [Display(Name = "Đã trả phòng")]
        CheckedOut = 4,

        [Display(Name = "Đã hủy")]
        Cancelled = 5
    }
}
