using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.DTOs;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Enums;
using Project_HotelBooking.Identity;
using Project_HotelBooking.ViewModels.Staff;

namespace Project_HotelBooking.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;
        private readonly UserManager<AppUser> _userManager;

        public StaffService(IStaffRepository repository,
                               UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<(bool Success, string Message, string? Username, string? TempPassword)> UpsertAsync(StaffUpsertDto dto, int adminId)
        {
            if (dto.Id == null)
            {
                var employeeCode = await GenerateEmployeeCode(dto.Role, dto.StartWorkingDate.Value);
                // CREATE
                var user = new AppUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FullName = dto.FullName,
                    EmployeeCode = employeeCode,
                    StartWorkingDate = dto.StartWorkingDate,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = adminId,
                    MustChangePassword = true
                };

                var tempPassword = "Emp@" + Guid.NewGuid().ToString("N").Substring(0, 8);

                var result = await _userManager.CreateAsync(user, tempPassword);

                if (!result.Succeeded)
                {
                    return (false, string.Join(",", result.Errors.Select(x => x.Description)), null, null);
                }

                await _userManager.AddToRoleAsync(user, dto.Role.ToString());

                return (true, "Tạo nhân viên thành công", user.UserName, tempPassword);
            }
            else
            {
                // UPDATE
                var user = await _userManager.FindByIdAsync(dto.Id.ToString());
                if (user == null)
                    return (false, "Không tìm thấy nhân viên", null, null);

                user.FullName = dto.FullName;
                user.Email = dto.Email;
                user.StartWorkingDate = dto.StartWorkingDate;
                user.IsActive = dto.IsActive;
                user.ModifiedAt = DateTime.Now;
                user.Address = dto.Address;
                user.Role = dto.Role;
                user.PhoneNumber = dto.PhoneNumber;
                user.DateOfBirth = dto.DateOfBirth;
                user.ModifiedByUserId = adminId;

                await _userManager.UpdateAsync(user);

                return (true, "Cập nhật thành công", null, null);
            }
        }

        public async Task<List<StaffListVM>> GetAllAsync()
        {
            var users = _userManager.Users
                .Where(x => !x.IsDeleted)
                .ToList();

            var result = new List<StaffListVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // ❌ Bỏ Admin
                if (user.Email == "admin@hotel.com")
                    continue;

                var roleName = roles.FirstOrDefault();

                // Default role nếu null
                UserRole roleEnum = UserRole.Receptionist;

                if (!string.IsNullOrEmpty(roleName) &&
                    Enum.TryParse<UserRole>(roleName, out var parsedRole))
                {
                    roleEnum = parsedRole;
                }

                result.Add(new StaffListVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    EmployeeCode = user.EmployeeCode,
                    Role = roleEnum
                });
            }

            return result;
        }

        public async Task<StaffUpsertDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();

            return new StaffUpsertDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FullName = user.FullName,
                EmployeeCode = user.EmployeeCode,
                StartWorkingDate = user.StartWorkingDate,
                Address = user.Address,
                Role = user.Role,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            };
        }


        // ==========================
        // Lấy prefix role
        // ==========================
        private string GetRolePrefix(UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "ADM",
                UserRole.Receptionist => "REC",
                UserRole.Accountant => "ACC",
                UserRole.Staff => "STA",
                _ => "EMP"
            };
        }

        // ==========================
        // Sinh mã nhân viên
        // ==========================
        private async Task<string> GenerateEmployeeCode(UserRole role, DateTime startWorkingDate)
        {
            var prefix = GetRolePrefix(role);

            var year = startWorkingDate.Year;

            var users = await _userManager.Users
                .Where(x => x.EmployeeCode.StartsWith($"{prefix}-{year}"))
                .ToListAsync();

            int number = 1;

            if (users.Any())
            {
                var lastNumber = users
                    .Select(x => int.Parse(x.EmployeeCode.Split('-')[2]))
                    .Max();

                number = lastNumber + 1;
            }

            return $"{prefix}-{year}-{number.ToString("D4")}";
        }
    }
}

