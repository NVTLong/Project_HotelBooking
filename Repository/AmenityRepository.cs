using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;
using System;
using System.Linq.Expressions;

namespace Project_HotelBooking.Repository
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _context;
        public AmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<Amenity?> GetByIdAsync(int id)
        {
            return await _context.Amenities.FindAsync(id);
        }
       
        public async Task CreateAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
            return _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Remove(amenity);
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Amenity, bool>> exception)
        {
            return await _context.Amenities.AnyAsync(exception);
        }

    }
}
