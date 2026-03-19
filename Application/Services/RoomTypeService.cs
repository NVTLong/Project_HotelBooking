using AutoMapper;
using Humanizer;
using Project_HotelBooking.Application.DTOs.RoomType;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IMapper _mapper;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomTypeDto>> GetAllAsync()
        {
            var  roomType = await _roomTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoomTypeDto>>(roomType);
        }

        public async Task<RoomTypeDto> GetByIdAsync(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                return null;
            }
            return _mapper.Map<RoomTypeDto>(roomType);
        }

        public async Task CreateAsync(RoomTypeCreateDto roomTypeCreateDto, int userIdCreate)
        {
            var roomType = _mapper.Map<RoomType>(roomTypeCreateDto);
            roomType.CreatedAt = DateTime.UtcNow;
            roomType.CreatedByUserId = userIdCreate;
            await _roomTypeRepository.CreateAsync(roomType);
        }

        public async Task UpdateAsync(RoomTypeUpdateDto roomTypeUpdateDto, int userId)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(roomTypeUpdateDto.Id);
            if (roomType == null)
            {
                throw new Exception("Không tồn tại loại phòng");
            }
            _mapper.Map(roomTypeUpdateDto, roomType);

            // thông tin audit
            roomType.ModifiedAt = DateTime.UtcNow;
            roomType.ModifiedByUserId = userId;

            await _roomTypeRepository.UpdateAsync(roomType);
        }

        public async Task DeleteAsync(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if(roomType != null)
            {
                await _roomTypeRepository.DeleteAsync(id);
                await _roomTypeRepository.SaveAsync();
            }
        }
    }
}
