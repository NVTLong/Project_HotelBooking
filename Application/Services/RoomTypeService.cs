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

        public async Task<IEnumerable<RoomTypeDto>> GetAvailableRoomTypesAsync(DateTime checkIn, DateTime checkOut, int guests)
        {
            var roomTypes = await _roomTypeRepository.GetAllWithRoomsAndBookingsAsync();

            var availableRoomTypes = roomTypes
                .Where(rt => rt.IsActive && rt.Capacity >= guests)
                .Select(rt => {
                    // đếm số phòng còn trống trong khoảng thời gian
                    var availableRoomsCount = rt.Rooms!.Count(r => 
                        r.IsActive && 
                        !r.BookingDetails!.Any(bd => 
                            bd.Booking!.Status != Enums.BookingStatus.Cancelled &&
                            !(bd.Booking.CheckOutDate <= checkIn || bd.Booking.CheckInDate >= checkOut)
                        )
                    );

                    if (availableRoomsCount > 0)
                    {
                        var dto = _mapper.Map<RoomTypeDto>(rt);
                        // có thể thêm số lượng phòng trống vào DTO nếu cần
                        return dto;
                    }
                    return null;
                })
                .Where(dto => dto != null)
                .Cast<RoomTypeDto>()
                .ToList();

            return availableRoomTypes;
        }

        public async Task<RoomTypeDto?> GetDetailedRoomTypeAsync(int id)
        {
            var roomType = await _roomTypeRepository.GetByIdWithRoomsAndAmenitiesAsync(id);
            if (roomType == null) return null;

            var dto = _mapper.Map<RoomTypeDto>(roomType);
            
            // Lấy tất cả tiện ích duy nhất từ các phòng thuộc loại này
            if (roomType.Rooms != null)
            {
                dto.Amenities = roomType.Rooms
                    .SelectMany(r => r.RoomAmenities ?? new List<RoomAmenity>())
                    .Where(ra => ra.Amenity != null)
                    .Select(ra => ra.Amenity!.Name)
                    .Distinct()
                    .ToList();
            }

            return dto;
        }
    }
}
