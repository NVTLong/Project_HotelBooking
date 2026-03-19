using Project_HotelBooking.Models;
using System.Linq.Expressions;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task CreateAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(int id);
        Task SaveAsync();
        Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate);
    }
}
