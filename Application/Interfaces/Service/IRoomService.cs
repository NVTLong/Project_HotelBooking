using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(int id);
        Task CreateAsync(RoomCreateDto roomCreateDto, int userIdCreate);
        Task UpdateAsync(RoomUpdateDto roomUpdateDto, int userId);
        Task DeleteAsync(int id);
        Task<List<Room>> GetAvailableRoomsByTypeAsync(int roomTypeId);
    }
}
