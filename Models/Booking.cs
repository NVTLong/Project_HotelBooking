using Project_HotelBooking.Base;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Models
{
    public class Booking : AuditableEntity
    {
        public string BookingCode { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public BookingStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }
        public DateTime? CancelledAt { get; set; }

        public ICollection<BookingDetail>? BookingDetails { get; set; }
        public Payment? Payment { get; set; }
        public ICollection<BookingService>? BookingServices { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
