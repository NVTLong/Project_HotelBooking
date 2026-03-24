using Project_HotelBooking.Application.DTOs.Booking;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);

        Task CreateAsync(Booking booking);

        Task UpdateAsync(Booking booking);

        Task DeleteAsync(int id);
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId);
        Task SaveAsync();
    }
}
