using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IHotelServiceRepository
    {
        Task<IEnumerable<HotelService>> GetAllAsync();

        Task<HotelService?> GetByIdAsync(int id);

        Task CreateAsync(HotelService entity);

        Task UpdateAsync(HotelService entity);

        Task DeleteAsync(int id);

        Task SaveAsync();
    }
}
