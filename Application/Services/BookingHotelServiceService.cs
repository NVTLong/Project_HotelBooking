using AutoMapper;
using Project_HotelBooking.Application.DTOs;
using Project_HotelBooking.Application.DTOs.BookingHotelService;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

public class BookingHotelServiceService : IBookingHotelServiceService
{
    private readonly IBookingHotelServiceRepository _repository;
    private readonly IMapper _mapper;

    public BookingHotelServiceService(
        IBookingHotelServiceRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Thêm 1 service
    public async Task AddServiceAsync(BookingHotelServiceCreateDto dto, int userId)
    {
        var existing = (await _repository.GetByBookingDetailIdAsync(dto.BookingDetailId))
            .FirstOrDefault(x => x.ServiceId == dto.ServiceId);

        if (existing != null)
        {
            existing.Quantity += dto.Quantity;
            existing.SubTotal = existing.Price * existing.Quantity;
            await _repository.UpdateAsync(existing);
            return;
        }

        var entity = _mapper.Map<BookingHotelService>(dto);
        entity.SubTotal = dto.Price * dto.Quantity;
        entity.CreatedByUserId = userId;
        entity.CreatedAt = DateTime.Now;

        await _repository.CreateAsync(entity);
    }

    // Thêm nhiều service
    public async Task AddMultipleAsync(BookingHotelServiceMultipleDto dto, int userId)
    {
        var existingList = await _repository.GetByBookingDetailIdAsync(dto.BookingDetailId);

        foreach (var item in dto.Items)
        {
            var exist = existingList.FirstOrDefault(x => x.ServiceId == item.ServiceId);

            if (exist != null)
            {
                exist.Quantity += item.Quantity;
                exist.SubTotal = exist.Price * exist.Quantity;
                await _repository.UpdateAsync(exist);
            }
            else
            {
                var entity = _mapper.Map<BookingHotelService>(item);
                entity.BookingDetailId = dto.BookingDetailId;
                entity.SubTotal = item.Price * item.Quantity;
                entity.CreatedByUserId = userId;
                entity.CreatedAt = DateTime.Now;
                await _repository.CreateAsync(entity);
            }
        }

    }

    // Lấy danh sách service
    public async Task<List<BookingHotelServiceDetailDto>> GetByBookingDetailIdAsync(int bookingDetailId)
    {
        var data = await _repository.GetByBookingDetailIdAsync(bookingDetailId);

        return _mapper.Map<List<BookingHotelServiceDetailDto>>(data);
    }

    // Xóa service
    public async Task RemoveServiceAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
