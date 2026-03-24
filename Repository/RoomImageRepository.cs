using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class RoomImageRepository : IRoomImageRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RoomImage entity)
        {
            await _context.Set<RoomImage>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByRoomId(int roomId)
        {
            var images = await _context.Set<RoomImage>().Where(x => x.RoomId == roomId).ToListAsync();
            _context.Set<RoomImage>().RemoveRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomImage>> GetByRoomId(int roomId)
        {
            return await _context.Set<RoomImage>().Where(x => x.RoomId == roomId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
