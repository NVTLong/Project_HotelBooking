using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IFloorRepository
    {
        Task<IEnumerable<Floor>> GetAllAsync();
        Task<Floor?> GetByIdAsync(int id);
        Task CreateAsync(Floor floor);
        Task UpdateAsync(Floor floor);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
