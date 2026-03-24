using Project_HotelBooking.Application.DTOs;
using Project_HotelBooking.ViewModels.Staff;

namespace Project_HotelBooking.Application.Interfaces.Service
{
    public interface IStaffService
    {
        Task<(bool Success, string Message, string? Username, string? TempPassword)> UpsertAsync(StaffUpsertDto dto, int adminId);
        Task<List<StaffListVM>> GetAllAsync();
        Task<StaffUpsertDto?> GetByIdAsync(int id);
    }
}

