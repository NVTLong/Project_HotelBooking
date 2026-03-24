using Project_HotelBooking.Application.DTOs.Amenity;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityDto>> GetAllAsync();
        Task<AmenityDto> GetByIdAsync(int id);
        Task CreateAsync(AmenityDto amenityDto, int userIdCreate);
        Task UpdateAsync(AmenityUpdateDto amenityUpdateDto, int userId);
        Task DeleteAsync(int id);
        Task<List<AmenityDto>> GetDropDownAsync();
    }
}
