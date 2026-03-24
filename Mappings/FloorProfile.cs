using AutoMapper;
using Project_HotelBooking.Application.DTOs.Floor;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class FloorProfile : Profile
    {
        public FloorProfile()
        {
            CreateMap<Floor, FloorDto>();

            CreateMap<CreateFloorDto, Floor>();

            CreateMap<UpdateFloorDto, Floor>().ReverseMap();
        }
    }
}
