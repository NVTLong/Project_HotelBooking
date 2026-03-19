using Project_HotelBooking.Application.DTOs.Service;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IHotelServiceService
    {
        Task<IEnumerable<HotelServiceDto>> GetAllAsync();

        Task<HotelServiceDto?> GetByIdAsync(int id);

        Task CreateAsync(HotelServiceCreateDto dto, int userId);

        Task UpdateAsync(HotelServiceUpdateDto dto, int userId);

        Task DeleteAsync(int id);
    }
}
