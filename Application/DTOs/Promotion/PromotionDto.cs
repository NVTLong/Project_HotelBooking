using System;
using System.ComponentModel.DataAnnotations;

namespace Project_HotelBooking.Application.DTOs.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã ưu đãi không được để trống")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Số tiền giảm không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền giảm phải lớn hơn hoặc bằng 0")]
        public decimal DiscountAmount { get; set; }

        public bool IsPercentage { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class PromotionUpdateDto : PromotionDto
    {
    }
}
