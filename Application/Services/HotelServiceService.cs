using AutoMapper;
using NuGet.Protocol.Core.Types;
using Project_HotelBooking.Application.DTOs.Service;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class HotelServiceService : IHotelServiceService
    {
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly IMapper _mapper;
        public HotelServiceService(IHotelServiceRepository hotelServiceRepository, IMapper mapper)
        {
            _hotelServiceRepository = hotelServiceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelServiceDto>> GetAllAsync()
        {
            var services = await _hotelServiceRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<HotelServiceDto>>(services);
        }

        public async Task<HotelServiceDto?> GetByIdAsync(int id)
        {
            var service = await _hotelServiceRepository.GetByIdAsync(id);

            if (service == null)
                return null;

            return _mapper.Map<HotelServiceDto>(service);
        }

        public async Task CreateAsync(HotelServiceCreateDto dto, int userId)
        {
            var entity = _mapper.Map<HotelService>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedByUserId = userId;

            await _hotelServiceRepository.CreateAsync(entity);

            await _hotelServiceRepository.SaveAsync();
        }

        public async Task UpdateAsync(HotelServiceUpdateDto dto, int userId)
        {
            var entity = _mapper.Map<HotelService>(dto);

            entity.ModifiedAt = DateTime.UtcNow;
            entity.ModifiedByUserId = userId;

            await _hotelServiceRepository.UpdateAsync(entity);

            await _hotelServiceRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _hotelServiceRepository.DeleteAsync(id);

            await _hotelServiceRepository.SaveAsync();
        }
    }
}
