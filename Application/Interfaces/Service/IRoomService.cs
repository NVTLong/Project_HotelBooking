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
        Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, int guests);
        Task<IEnumerable<RoomDto>> GetSuggestionsAsync(int currentRoomId, int limit = 3);
        Task<IEnumerable<RoomDto>> GetRoomsWithSamePriceAsync(int currentRoomId);
        Task<IEnumerable<RoomDto>> GetRoomsOfOtherTypesAsync(int currentRoomId);
    }
}
