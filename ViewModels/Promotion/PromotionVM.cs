using System;
using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.ViewModels.Promotion
{
    public class PromotionVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã ưu đãi")]
        [Display(Name = "Mã ưu đãi")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập giá trị giảm")]
        [Display(Name = "Giá trị giảm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Giảm theo phần trăm?")]
        public bool IsPercentage { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActive { get; set; } = true;
    }
}
