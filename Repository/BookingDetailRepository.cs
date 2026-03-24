using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDetail>> GetByBookingIdAsync(int bookingID)
        {
            return await _context.BookingDetails
                .Include(x => x.Room)
                .Include(x => x.BookingHotelServices) 
                    .ThenInclude(x => x.HotelService)
                .Where(x => x.BookingId == bookingID)
                .ToListAsync();
        }

        public async Task<BookingDetail?> GetByIdAsync(int id)
        {
            return await _context.BookingDetails
                .Include(x => x.Room)
                .Include(x => x.BookingHotelServices)
                    .ThenInclude(x => x.HotelService)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(BookingDetail entity)
        {
            await _context.BookingDetails.AddAsync(entity);
        }

        public async Task UpdateAsync(BookingDetail entity)
        {
            _context.BookingDetails.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bkdt = await _context.BookingDetails.FindAsync(id);
            if (bkdt != null)
            {
                _context.Remove(bkdt);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsRoomBookedAsync(int roomId)
        {
            return await _context.BookingDetails
                .AnyAsync(x => x.RoomId == roomId);
        }

    }
}
