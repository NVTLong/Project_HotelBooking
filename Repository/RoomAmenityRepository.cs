using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Data;
using Project_HotelBooking.Models;
using System;

namespace Project_HotelBooking.Repository
{
    public class RoomAmenityRepository : IRoomAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomAmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RoomAmenity roomAmenity)
        {
            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByRoomId(int roomId)
        {
            var items = await _context.RoomAmenities
                .Where(x => x.RoomId == roomId)
                .ToListAsync();

            _context.RoomAmenities.RemoveRange(items);

            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetAmenityIdsByRoomId(int roomId)
        {
            return await _context.RoomAmenities
                .Where(x => x.RoomId == roomId)
                .Select(x => x.AmenityId)
                .ToListAsync();
        }
    }

}
