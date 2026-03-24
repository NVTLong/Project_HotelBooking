using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class BookingHotelServiceRepository : IBookingHotelServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingHotelServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingHotelService>> GetAllAsync()
        {
            return await _context.BookingHotelServices
                .Include(x => x.HotelService)
                .ToListAsync();
        }

        public async Task<BookingHotelService?> GetByIdAsync(int id)
        {
            return await _context.BookingHotelServices
                .Include(x => x.HotelService)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BookingHotelService>> GetByBookingDetailIdAsync(int bookingDetailId)
        {
            return await _context.BookingHotelServices
                .Where(x => x.BookingDetailId == bookingDetailId)
                .Include(x => x.HotelService)
                .ToListAsync();
        }

        public async Task CreateAsync(BookingHotelService bookingService)
        {
            await _context.BookingHotelServices.AddAsync(bookingService);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookingHotelService bookingService)
        {
            _context.BookingHotelServices.Update(bookingService);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.BookingHotelServices.FindAsync(id);

            if (entity != null)
            {
                _context.BookingHotelServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
