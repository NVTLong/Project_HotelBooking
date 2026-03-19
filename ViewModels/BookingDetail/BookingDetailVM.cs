namespace Project_HotelBooking.ViewModels.BookingDetail
{
    public class BookingDetailVM
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public string? RoomNumber { get; set; }

        public decimal PricePerNight { get; set; }

        public int NumberOfNights { get; set; }

        public int NumberOfGuests { get; set; }

        public decimal SubTotal { get; set; }
    }
}
