using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IBookingDetailRepository
    {
        Task<List<BookingDetail>> GetByBookingIdAsync(int bookingID);

        Task<BookingDetail?> GetByIdAsync(int id);

        Task CreateAsync(BookingDetail entity);

        Task UpdateAsync(BookingDetail entity);

        Task DeleteAsync(int id);

        Task SaveAsync();

        public Task<bool> IsRoomBookedAsync(int roomId);
    }
}
