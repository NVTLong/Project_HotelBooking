using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Chưa thanh toán")]
        Unpaid = 1,
        [Display(Name = "Đã thanh toán")]
        Paid = 2,
        [Display(Name = "Đã hoàn tiền")]
        Refunded = 3
    }
}
