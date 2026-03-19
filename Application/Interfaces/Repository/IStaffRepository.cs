using Microsoft.AspNetCore.Identity;
using Project_HotelBooking.Identity;

namespace Project_HotelBooking.Application.Interfaces.Repository
{
    public interface IStaffRepository
    {
        Task<AppUser?> GetByIdAsync(int id);
        Task<IQueryable<AppUser?>> GetAllAsync();
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> UpdateAsync(AppUser user);
        Task AddToRoleAsync(AppUser user, string role);
        Task RemoveFromRolesAsync(AppUser user);
    }
}
