using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IRoomAmenityRepository
    {
        Task CreateAsync(RoomAmenity roomAmenity);

        Task DeleteByRoomId(int roomId);

        Task<List<int>> GetAmenityIdsByRoomId(int roomId);
    }

}
