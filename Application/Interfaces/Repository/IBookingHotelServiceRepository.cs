using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IBookingHotelServiceRepository
    {
        Task<List<BookingHotelService>> GetAllAsync();

        Task<BookingHotelService?> GetByIdAsync(int id);

        Task<List<BookingHotelService>> GetByBookingDetailIdAsync(int bookingDetailId);

        Task CreateAsync(BookingHotelService bookingService);

        Task UpdateAsync(BookingHotelService bookingService);

        Task DeleteAsync(int id);
    }
}
