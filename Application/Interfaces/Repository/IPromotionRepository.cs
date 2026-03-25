using Project_HotelBooking.Models;
using System.Linq.Expressions;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int id);
        Task CreateAsync(Promotion promotion);
        Task UpdateAsync(Promotion promotion);
        Task DeleteAsync(int id);
        Task SaveAsync();
        Task<bool> AnyAsync(Expression<Func<Promotion, bool>> exception);
    }
}
