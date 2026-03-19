using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.Enums
{
    public enum RoomStatus
    {
        [Display(Name = "Trống")]
        Available = 1,
        [Display(Name = "Đang có khách")]
        Occupied = 2,
        [Display(Name = "Bảo trì")]
        Maintenance = 3,
        [Display(Name = "Đang dọn dẹp")]
        Cleaning = 4,
        [Display(Name = "Đang giữ cho booking")]
        Reserved = 5,
    }
}
