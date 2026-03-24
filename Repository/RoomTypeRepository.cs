using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomType>> GetAllAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        public async Task<RoomType?> GetByIdAsync(int id)
        {
            return await _context.RoomTypes
                .Include(x => x.RoomTypeImages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            return _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType != null)
            {
                _context.Remove(roomType);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoomType>> GetAllWithRoomsAndBookingsAsync()
        {
            return await _context.RoomTypes
                .Include(x => x.RoomTypeImages)
                .Include(x => x.Rooms)
                    .ThenInclude(r => r.BookingDetails)
                        .ThenInclude(bd => bd.Booking)
                .ToListAsync();
        }

        public async Task<RoomType?> GetByIdWithRoomsAndAmenitiesAsync(int id)
        {
            return await _context.RoomTypes
                .Include(x => x.RoomTypeImages)
                .Include(x => x.Rooms)
                    .ThenInclude(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
