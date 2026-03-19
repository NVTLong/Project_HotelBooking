using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync ();
        Task<RoomType?> GetByIdAsync (int id);
        Task CreateAsync (RoomType roomType);
        Task UpdateAsync (RoomType roomType);
        Task DeleteAsync (int id);
        Task SaveAsync();
    }
}
