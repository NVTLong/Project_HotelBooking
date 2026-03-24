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
        private readonly IRoomImageRepository _roomImageRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, 
                            IRoomAmenityRepository roomAmenityRepository,
                            IRoomImageRepository roomImageRepository,
                            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _roomAmenityRepository = roomAmenityRepository;
            _roomImageRepository = roomImageRepository;
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

            if (room.RoomImages != null)
            {
                dto.RoomImages = _mapper.Map<List<RoomImageDto>>(room.RoomImages);
            }

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

            // ===== LƯU ROOM IMAGES =====
            if (roomCreateDto.ImageUrls != null)
            {
                foreach (var url in roomCreateDto.ImageUrls)
                {
                    var roomImage = new RoomImage
                    {
                        RoomId = room.Id,
                        ImageUrl = url,
                        IsPrimary = url == roomCreateDto.ImageUrls.First()
                    };
                    await _roomImageRepository.CreateAsync(roomImage);
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

            // ===== CẬP NHẬT ROOM IMAGES =====
            if (roomUpdateDto.ImageUrls != null && roomUpdateDto.ImageUrls.Any())
            {
                await _roomImageRepository.DeleteByRoomId(room.Id);
                foreach (var url in roomUpdateDto.ImageUrls)
                {
                    var roomImage = new RoomImage
                    {
                        RoomId = room.Id,
                        ImageUrl = url,
                        IsPrimary = url == roomUpdateDto.ImageUrls.First()
                    };
                    await _roomImageRepository.CreateAsync(roomImage);
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

        public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut, int guests)
        {
            var rooms = await _roomRepository.GetAvailableRoomsWithBookingsAsync();

            var availableRooms = rooms
                .Where(r => r.IsActive && r.RoomType!.Capacity >= guests)
                .Where(r => !r.BookingDetails!.Any(bd =>
                    bd.Booking!.Status != Enums.BookingStatus.Cancelled &&
                    !(bd.Booking.CheckOutDate <= checkIn || bd.Booking.CheckInDate >= checkOut)
                ))
                .ToList();

            var dtos = _mapper.Map<IEnumerable<RoomDto>>(availableRooms);
            
            // Map RoomImages separately if needed, though Automapper should handle it if configured
            foreach(var dto in dtos)
            {
                var room = availableRooms.First(x => x.Id == dto.Id);
                if (room.RoomImages != null)
                {
                    dto.RoomImages = _mapper.Map<List<RoomImageDto>>(room.RoomImages);
                }
            }

            return dtos;
        }

        public async Task<IEnumerable<RoomDto>> GetSuggestionsAsync(int currentRoomId, int limit = 3)
        {
            var currentRoom = await _roomRepository.GetByIdAsync(currentRoomId);
            if (currentRoom == null) return Enumerable.Empty<RoomDto>();

            var allRooms = await _roomRepository.GetAvailableRoomsWithBookingsAsync();
            
            // Lấy các phòng khác cùng loại hoặc khác, ưu tiên cùng loại
            var suggestions = allRooms
                .Where(r => r.Id != currentRoomId && r.IsActive && r.Status == RoomStatus.Available)
                .OrderBy(r => r.RoomTypeId == currentRoom.RoomTypeId ? 0 : 1)
                .Take(limit)
                .ToList();

            return _mapper.Map<IEnumerable<RoomDto>>(suggestions);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsWithSamePriceAsync(int currentRoomId)
        {
            var currentRoom = await _roomRepository.GetRoomWithAmenitiesAsync(currentRoomId);
            if (currentRoom == null) return Enumerable.Empty<RoomDto>();

            var allRooms = await _roomRepository.GetAvailableRoomsWithBookingsAsync();
            
            var samePrice = allRooms
                .Where(r => r.Id != currentRoomId && 
                            r.IsActive && 
                            r.RoomType!.BasePrice == currentRoom.RoomType!.BasePrice &&
                            r.FloorId != currentRoom.FloorId)
                .ToList();

            return _mapper.Map<IEnumerable<RoomDto>>(samePrice);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsOfOtherTypesAsync(int currentRoomId)
        {
            var currentRoom = await _roomRepository.GetRoomWithAmenitiesAsync(currentRoomId);
            if (currentRoom == null) return Enumerable.Empty<RoomDto>();

            var allRooms = await _roomRepository.GetAvailableRoomsWithBookingsAsync();
            
            var otherTypes = allRooms
                .Where(r => r.Id != currentRoomId && 
                            r.IsActive && 
                            r.RoomTypeId != currentRoom.RoomTypeId)
                .GroupBy(r => r.RoomTypeId)
                .Select(g => g.First()) // Lấy một phòng đại diện cho mỗi loại khác
                .ToList();

            return _mapper.Map<IEnumerable<RoomDto>>(otherTypes);
        }
    }
}
