using AutoMapper;
using Project_HotelBooking.Application.DTOs.Promotion;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        public PromotionService(IPromotionRepository promotionRepository, IMapper mapper)
        {
            _promotionRepository = promotionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PromotionDto>> GetAllAsync()
        {
            var promotions = await _promotionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PromotionDto>>(promotions);
        }

        public async Task<PromotionDto?> GetByIdAsync(int id)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion == null) return null;
            return _mapper.Map<PromotionDto>(promotion);
        }

        public async Task CreateAsync(PromotionDto promotionDto, int userIdCreate)
        {
            var exists = await _promotionRepository.AnyAsync(p => p.Code == promotionDto.Code);
            if (exists)
            {
                throw new Exception("Mã ưu đãi này đã tồn tại");
            }

            var promotion = _mapper.Map<Promotion>(promotionDto);
            promotion.CreatedAt = DateTime.UtcNow;
            promotion.CreatedByUserId = userIdCreate;

            await _promotionRepository.CreateAsync(promotion);
        }

        public async Task UpdateAsync(PromotionUpdateDto promotionUpdateDto, int userId)
        {
            var promotion = await _promotionRepository.GetByIdAsync(promotionUpdateDto.Id);
            if (promotion == null)
            {
                throw new Exception("Không tìm thấy mã ưu đãi");
            }

            _mapper.Map(promotionUpdateDto, promotion);
            promotion.ModifiedAt = DateTime.UtcNow;
            promotion.ModifiedByUserId = userId;

            await _promotionRepository.UpdateAsync(promotion);
        }

        public async Task DeleteAsync(int id)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion != null)
            {
                await _promotionRepository.DeleteAsync(id);
                await _promotionRepository.SaveAsync();
            }
        }
    }
}
