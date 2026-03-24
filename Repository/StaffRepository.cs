using Microsoft.AspNetCore.Identity;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Identity;

namespace Project_HotelBooking.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public StaffRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IQueryable<AppUser?>> GetAllAsync()
        {
            return  _userManager.Users;
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task AddToRoleAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task RemoveFromRolesAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
