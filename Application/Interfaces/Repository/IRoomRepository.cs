using Project_HotelBooking.Models;
using System.Linq.Expressions;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task CreateAsync (Room room);
        Task UpdateAsync (Room room);
        Task DeleteAsync (int id);
        Task SaveAsync();
        Task<bool> AnyAsync(Expression<Func<Room, bool>> predicate);
        Task<Room?> GetRoomWithAmenitiesAsync(int id);

    }
}
