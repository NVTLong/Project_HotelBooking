using Microsoft.AspNetCore.Mvc;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.ViewComponents
{
    public class RoomTypeFooterViewComponent : ViewComponent
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeFooterViewComponent(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roomTypes = await _roomTypeRepository.GetAllWithRoomsAndBookingsAsync();
            var activeRoomTypes = roomTypes.Where(rt => rt.IsActive).OrderBy(rt => rt.BasePrice).ToList();
            
            return View(activeRoomTypes);
        }
    }
}
