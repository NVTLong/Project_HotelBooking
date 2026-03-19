using Project_HotelBooking.Application.DTOs.Customer;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int id);

        Task CreateAsync(CustomerCreateDto dto, int userIdCreate);

        Task UpdateAsync(CustomerUpdateDto dto, int userId);

        Task DeleteAsync(int id);
    }
}
