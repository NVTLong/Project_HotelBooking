using Project_HotelBooking.Base;
using Project_HotelBooking.Enums;

namespace Project_HotelBooking.Models 
{ 
    public class BookingDetail : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public decimal PricePerNight { get; set; }
        public int NumberOfNights { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal SubTotal { get; set; }
        public ICollection<BookingHotelService> BookingHotelServices { get; set; }

    }
}
