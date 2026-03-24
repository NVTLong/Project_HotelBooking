using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Data;

namespace Project_HotelBooking.ViewComponents
{
    public class RoomTypeMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RoomTypeMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roomTypes = await _context.RoomTypes
                .Where(rt => rt.IsActive)
                .OrderBy(rt => rt.Name)
                .ToListAsync();

            return View(roomTypes);
        }
    }
}
