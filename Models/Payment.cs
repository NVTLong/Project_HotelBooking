using Project_HotelBooking.Base;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Models
{
    public class Payment : AuditableEntity
    {
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public string PaymentMethod { get; set; } = null!;
        public string? TransactionCode { get; set; }

        public string? GatewayResponse { get; set; }

        public DateTime? PaidAt { get; set; }
        public DateTime? RefundedAt { get; set; }
    }

}
