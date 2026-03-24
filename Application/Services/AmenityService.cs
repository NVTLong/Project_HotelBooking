using AutoMapper;
using Project_HotelBooking.Application.DTOs.Amenity;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;
        public AmenityService(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository; 
            _mapper = mapper;
        }
        public async Task<IEnumerable<AmenityDto>> GetAllAsync()
        {
            var amenity = await _amenityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AmenityDto>>(amenity);
        }

        public async Task<AmenityDto> GetByIdAsync(int id)
        {
            var amenity = await _amenityRepository.GetByIdAsync(id);
            if (amenity == null)
            {
                return null;
            }
            return _mapper.Map<AmenityDto>(amenity);
        }

        public async Task CreateAsync(AmenityDto amenityDto, int userIdCreate)
        {
            // Kiểm tra dịch vụ đã có chưa
            var exists = await _amenityRepository.AnyAsync(a => a.Name == amenityDto.Name);

            if (exists)
            {
                throw new Exception("Đã có dịch vụ này");
            }

            var amenity = _mapper.Map<Amenity>(amenityDto);
            amenity.CreatedAt = DateTime.UtcNow;
            amenity.CreatedByUserId = userIdCreate;

            await _amenityRepository.CreateAsync(amenity);
        }

        public async Task UpdateAsync(AmenityUpdateDto amenityUpdateDto, int userId)
        {
            var amenity = await _amenityRepository.GetByIdAsync(amenityUpdateDto.Id);
            if (amenity == null)
            {
                throw new Exception("Không có dịch vụ này");
            }
            _mapper.Map(amenityUpdateDto, amenity);

            // nguoi thay doi
            amenity.ModifiedAt = DateTime.UtcNow;
            amenity.ModifiedByUserId = userId;

            await _amenityRepository.UpdateAsync(amenity);
        }

        public async Task DeleteAsync(int id)
        {
            var amenity = await _amenityRepository.GetByIdAsync(id);
            if (amenity != null)
            {
                await _amenityRepository.DeleteAsync(id);
                await _amenityRepository.SaveAsync();
            }
        }

        public Task<List<AmenityDto>> GetDropDownAsync()
        {
            throw new NotImplementedException();
        }
    }
}
