using Project_HotelBooking.Application.DTOs.RoomType;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomTypeDto>> GetAllAsync();
        Task<RoomTypeDto> GetByIdAsync(int id);
        Task CreateAsync (RoomTypeCreateDto roomTypeCreateDto, int userIdCreate);
        Task UpdateAsync (RoomTypeUpdateDto roomTypeUpdateDto, int userId);
        Task DeleteAsync (int id);
    }
}
