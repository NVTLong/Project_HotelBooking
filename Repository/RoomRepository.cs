using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;
using System.Linq.Expressions;

namespace Project_HotelBooking.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Floor)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task CreateAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }


        public Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            return _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Remove(room);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Check trùng
        public async Task<bool> AnyAsync(Expression<Func<Room, bool>> predicate)
        {
            return await _context.Rooms.AnyAsync(predicate);
        }

        public async Task<Room?> GetRoomWithAmenitiesAsync(int id)
        {
            return await _context.Rooms
                .Include(x => x.RoomAmenities)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
