using AutoMapper;
using Project_HotelBooking.Application.DTOs.BookingDetail;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository _repository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public BookingDetailService(
            IBookingDetailRepository repository,
            IRoomRepository roomRepository,
            IMapper mapper)
        {
            _repository = repository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<List<BookingDetailDto>> GetByBookingIdAsync(int bookingID)
        {
            var data = await _repository.GetByBookingIdAsync(bookingID);
            return _mapper.Map<List<BookingDetailDto>>(data);
        }

        public async Task<BookingDetailDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<BookingDetailDto>(entity);
        }

        public async Task CreateAsync(BookingDetailCreateDto dto, int userId)
        {
            // 1 kiểm tra phòng
            var room = await _roomRepository.GetByIdAsync(dto.RoomId);

            if (room == null)
                throw new Exception("Room not found");

            if (room.Status != RoomStatus.Available)
                throw new Exception("Room is not available");

            // 2 map dto → entity
            var entity = _mapper.Map<BookingDetail>(dto);

            // 3 tính tiền
            entity.SubTotal = dto.PricePerNight * dto.NumberOfNights;

            // 4 lưu booking detail
            await _repository.CreateAsync(entity);

            // 5 đổi trạng thái phòng
            room.Status = RoomStatus.Reserved;

            await _roomRepository.UpdateAsync(room);

            // 6 save database
            await _repository.SaveAsync();
        }


        public async Task UpdateAsync(BookingDetailUpdateDto dto, int userId)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
                throw new Exception("BookingDetail not found");

            _mapper.Map(dto, entity);

            entity.SubTotal = dto.PricePerNight * dto.NumberOfNights;

            await _repository.UpdateAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                throw new Exception("BookingDetail not found");

            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
