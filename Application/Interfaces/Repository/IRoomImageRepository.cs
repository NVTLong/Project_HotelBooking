using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IRoomImageRepository
    {
        Task CreateAsync(RoomImage entity);
        Task DeleteByRoomId(int roomId);
        Task<List<RoomImage>> GetByRoomId(int roomId);
        Task SaveAsync();
    }
}
