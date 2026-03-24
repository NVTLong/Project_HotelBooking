using Project_HotelBooking.Application.DTOs.BookingHotelService;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IBookingHotelServiceService
    {
        Task AddServiceAsync(BookingHotelServiceCreateDto dto, int userId);
        Task AddMultipleAsync(BookingHotelServiceMultipleDto dto, int userId);
        Task<List<BookingHotelServiceDetailDto>> GetByBookingDetailIdAsync(int bookingDetailId);
        Task RemoveServiceAsync(int id);
    }
}
