using Project_HotelBooking.Application.DTOs.Floor;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IFloorService
    {
        Task<IEnumerable<FloorDto>> GetAllAsync();
        Task<FloorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateFloorDto createFloorDto);
        Task UpdateAsync(UpdateFloorDto updateFloorDto);
        Task DeleteAsync(int id);
    }
}
