using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class FloorRepository : IFloorRepository
    {
        private readonly ApplicationDbContext _context;
        public FloorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Floor>> GetAllAsync()
        {
            return await _context.Floors.ToListAsync();
        }

        public async Task<Floor?> GetByIdAsync(int id)
        {
            return await _context.Floors.FindAsync(id);
        }

        public async Task CreateAsync(Floor floor)
        {
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var floor = await _context.Floors.FindAsync(id);
            if (floor != null)
            {
                _context.Remove(floor);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
