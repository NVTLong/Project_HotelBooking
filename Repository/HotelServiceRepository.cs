using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Repository
{
    public class HotelServiceRepository : IHotelServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public HotelServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelService>> GetAllAsync()
        {
            return await _context.HotelServices.ToListAsync();
        }

        public async Task<HotelService?> GetByIdAsync(int id)
        {
            return await _context.HotelServices.FindAsync(id);
        }

        public async Task CreateAsync(HotelService entity)
        {
            await _context.HotelServices.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HotelService entity)
        {
            _context.HotelServices.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await GetByIdAsync(id);

            if (service != null)
                _context.HotelServices.Remove(service);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
