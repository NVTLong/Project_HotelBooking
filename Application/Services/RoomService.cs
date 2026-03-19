using AutoMapper;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Application.DTOs.RoomType;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;
using Project_HotelBooking.Models;
using Project_HotelBooking.Repository;

namespace Project_HotelBooking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomAmenityRepository _roomAmenityRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, 
                            IRoomAmenityRepository roomAmenityRepository,
                            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomAmenityRepository = roomAmenityRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var room = await _roomRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoomDto>>(room);
        }

        public async Task<RoomDto?> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomWithAmenitiesAsync(id);

            if (room == null)
                return null;

            var dto = _mapper.Map<RoomDto>(room);

            dto.AmenityIds = room.RoomAmenities
                .Select(x => x.AmenityId)
                .ToList();

            return dto;
        }


        public async Task CreateAsync(RoomCreateDto roomCreateDto, int userIdCreate)
        {
            // kiểm tra phòng đã tồn tại chưa
            var exists = await _roomRepository.AnyAsync(r =>
                r.RoomNumber == roomCreateDto.RoomNumber
                && r.FloorId == roomCreateDto.FloorId);

            if (exists)
            {
                throw new Exception("Số phòng này đã tồn tại trong tầng.");
            }

            var room = _mapper.Map<Room>(roomCreateDto);
            room.CreatedByUserId = userIdCreate;
            room.CreatedAt = DateTime.UtcNow;

            await _roomRepository.CreateAsync(room);

            // ===== LƯU ROOM AMENITY =====
            if (roomCreateDto.AmenityIds != null)
            {
                foreach (var amenityId in roomCreateDto.AmenityIds)
                {
                    var roomAmenity = new RoomAmenity
                    {
                        RoomId = room.Id,
                        AmenityId = amenityId
                    };

                    await _roomAmenityRepository.CreateAsync(roomAmenity);
                }
            }
        }

        public async Task UpdateAsync(RoomUpdateDto roomUpdateDto, int userId)
        {
            var room = await _roomRepository.GetByIdAsync(roomUpdateDto.Id);
            if (room == null)
            {
                throw new Exception("Không tồn tại loại phòng");
            }
            _mapper.Map(roomUpdateDto, room);

            // thông tin audit
            room.ModifiedAt = DateTime.UtcNow;
            room.ModifiedByUserId = userId;

            await _roomRepository.UpdateAsync(room);

            // ===== XÓA ROOM AMENITY CŨ =====
            await _roomAmenityRepository.DeleteByRoomId(room.Id);

            // ===== INSERT LẠI =====
            if (roomUpdateDto.AmenityIds != null)
            {
                foreach (var amenityId in roomUpdateDto.AmenityIds)
                {
                    var roomAmenity = new RoomAmenity
                    {
                        RoomId = room.Id,
                        AmenityId = amenityId
                    };

                    await _roomAmenityRepository.CreateAsync(roomAmenity);
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            var roomType = await _roomRepository.GetByIdAsync(id);
            if (roomType != null)
            {
                await _roomRepository.DeleteAsync(id);
                await _roomRepository.SaveAsync();
            }
        }

        public async Task<List<Room>> GetAvailableRoomsByTypeAsync(int roomTypeId)
        {
            var rooms = await _roomRepository.GetAllAsync();

            return rooms
                .Where(x => x.RoomTypeId == roomTypeId
                         && x.Status == RoomStatus.Available
                         && x.IsActive)
                .ToList();
        }

    }
}
