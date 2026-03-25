using Project_HotelBooking.Application.DTOs.Promotion;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionDto>> GetAllAsync();
        Task<PromotionDto?> GetByIdAsync(int id);
        Task CreateAsync(PromotionDto promotionDto, int userIdCreate);
        Task UpdateAsync(PromotionUpdateDto promotionUpdateDto, int userId);
        Task DeleteAsync(int id);
    }
}
