using Project_HotelBooking.Application.DTOs.BookingDetail;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IBookingDetailService
    {
        Task<List<BookingDetailDto>> GetByBookingIdAsync(int bookingID);

        Task<BookingDetailDto?> GetByIdAsync(int id);

        Task CreateAsync(BookingDetailCreateDto dto, int userId);

        Task UpdateAsync(BookingDetailUpdateDto dto, int userId);

        Task DeleteAsync(int id);
    }
}
