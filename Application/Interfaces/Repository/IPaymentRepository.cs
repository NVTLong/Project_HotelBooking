using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByBookingIdAsync(int bookingId);
        Task CreateAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task SaveAsync();
    }
}
