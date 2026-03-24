using Project_HotelBooking.Base;

namespace Project_HotelBooking.Models
{
    public class Promotion : AuditableEntity
    {
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public bool IsPercentage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
