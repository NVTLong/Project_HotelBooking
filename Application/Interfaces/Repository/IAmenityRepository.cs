using Project_HotelBooking.Models;
using System.Linq.Expressions;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
        Task<Amenity?> GetByIdAsync(int id);
        Task CreateAsync(Amenity amenity);
        Task UpdateAsync(Amenity amenity);
        Task DeleteAsync(int id);
        Task SaveAsync();
        Task<bool> AnyAsync(Expression<Func<Amenity, bool>> exception);
    }
}
