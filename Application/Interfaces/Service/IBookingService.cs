using Project_HotelBooking.Application.DTOs.Booking;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllAsync();

        Task<BookingDto?> GetByIdAsync(int id);

        Task CreateAsync(BookingCreateDto dto, int userId);

        Task UpdateAsync(BookingUpdateDto dto, int userId);

        Task DeleteAsync(int id);
        Task CheckInAsync(int bookingId, int userId);
        Task CheckOutAsync(int bookingId, int userId);
        Task ConfirmAsync(int bookingId, int userId);
    }

}
