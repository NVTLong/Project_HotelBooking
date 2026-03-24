using AutoMapper;
using Project_HotelBooking.Application.DTOs.Booking;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;
using Project_HotelBooking.Models;
using Project_HotelBooking.Repository;

namespace Project_HotelBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, 
                              IMapper mapper,
                              IBookingDetailRepository bookingDetailRepository,
                              IRoomRepository roomRepository,
                              IPaymentRepository paymentRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _bookingDetailRepository = bookingDetailRepository;
            _roomRepository = roomRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            return bookings.Select(x => new BookingDto
            {
                Id = x.Id,
                BookingCode = x.BookingCode,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer!.FullName,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                TotalGuests = x.TotalGuests,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                Note = x.Note,
                RoomCount = x.BookingDetails != null ? x.BookingDetails.Count : 0
            });
        }

        public async Task<BookingDto?> GetByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
                return null;

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task CreateAsync(BookingCreateDto dto, int userId)
        {
            var booking = _mapper.Map<Booking>(dto);

            booking.BookingCode = await GenerateBookingCode();

            booking.Status = BookingStatus.Pending;

            booking.CreatedAt = DateTime.UtcNow;
            booking.CreatedByUserId = userId;

            await _bookingRepository.CreateAsync(booking);
            await _bookingRepository.SaveAsync();
        }

        public async Task UpdateAsync(BookingUpdateDto dto, int userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(dto.Id);

            if (booking == null)
                throw new Exception("Booking not found");

            _mapper.Map(dto, booking);

            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedByUserId = userId;

            await _bookingRepository.UpdateAsync(booking);
            await _bookingRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _bookingRepository.DeleteAsync(id);
            await _bookingRepository.SaveAsync();
        }

        private async Task<string> GenerateBookingCode()
        {
            var today = DateTime.Now.ToString("yyyyMMdd");

            var count = (await _bookingRepository.GetAllAsync())
                .Count(x => x.BookingCode.Contains(today));

            return $"BK{today}-{(count + 1).ToString("D4")}";
        }

        public async Task CheckInAsync(int bookingId, int userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Không tìm thấy đơn đặt phòng");

            if (booking.Status != BookingStatus.Confirmed)
                throw new Exception("Việc đặt phòng phải được xác nhận trước khi nhận phòng.");

            // lấy danh sách phòng trong booking
            var bookingDetails = await _bookingDetailRepository.GetByBookingIdAsync(bookingId);

            if (bookingDetails == null || !bookingDetails.Any())
                throw new Exception("Chưa có phòng được chỉ định.");

            // đổi trạng thái phòng
            foreach (var detail in bookingDetails)
            {
                if (detail.RoomId <= 0)
                    continue;

                var room = await _roomRepository.GetByIdAsync(detail.RoomId);

                if (room == null)
                    continue;

                room.Status = RoomStatus.Occupied;

                await _roomRepository.UpdateAsync(room);
            }

            // đổi trạng thái booking
            booking.Status = BookingStatus.CheckedIn;
            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedByUserId = userId;

            await _bookingRepository.UpdateAsync(booking);
            await _roomRepository.SaveAsync();
            await _bookingRepository.SaveAsync();
        }

        public async Task ConfirmAsync(int bookingId, int userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status != BookingStatus.Pending)
                throw new Exception("Booking cannot confirm");

            booking.Status = BookingStatus.Confirmed;
            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedByUserId = userId;

            await _bookingRepository.UpdateAsync(booking);
            await _bookingRepository.SaveAsync();
        }

        public async Task CheckOutAsync(int bookingId, int userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Không tìm thấy đơn đặt phòng");

            if (booking.Status != BookingStatus.CheckedIn)
                throw new Exception("Chưa Check-In không thể Check-Out");

            if (booking.BookingDetails == null || !booking.BookingDetails.Any())
                throw new Exception("Không có phòng để Check-Out");

            var bookingDetails = await _bookingDetailRepository.GetByBookingIdAsync(bookingId);

            foreach (var detail in bookingDetails)
            {
                if (detail.RoomId <= 0)
                    continue;

                var room = await _roomRepository.GetByIdAsync(detail.RoomId);

                if (room != null)
                {
                    room.Status = RoomStatus.Cleaning;
                    await _roomRepository.UpdateAsync(room);
                }
            }

            booking.Status = BookingStatus.CheckedOut;
            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedByUserId = userId;

            var total = await CalculateTotalAsync(bookingId);

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = total,
                Status = PaymentStatus.Unpaid,
                PaymentMethod = "Cash"
            };
            await _bookingRepository.UpdateAsync(booking);
            await _paymentRepository.CreateAsync(payment);

            await _roomRepository.SaveAsync();
            await _bookingRepository.SaveAsync();
            await _paymentRepository.SaveAsync();
        }

        private async Task<decimal> CalculateTotalAsync(int bookingId)
        {
            decimal total = 0;

            // 1. Tiền phòng
            var bookingDetails = await _bookingDetailRepository.GetByBookingIdAsync(bookingId);

            foreach (var detail in bookingDetails)
            {
                total += detail.PricePerNight * detail.NumberOfNights;
            }

            // 2. Tiền dịch vụ
            foreach (var detail in bookingDetails)
            {
                if (detail.BookingHotelServices != null)
                {
                    foreach (var s in detail.BookingHotelServices)
                    {
                        total += s.Price * s.Quantity;
                    }
                }
            }

            return total;
        }

        public async Task<IEnumerable<BookingDto>> GetByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepository.GetByCustomerIdAsync(customerId);
            return bookings.Select(x => new BookingDto
            {
                Id = x.Id,
                BookingCode = x.BookingCode,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer!.FullName,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate,
                TotalGuests = x.TotalGuests,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                Note = x.Note,
                RoomCount = x.BookingDetails != null ? x.BookingDetails.Count : 0
            });
        }
    }
}
