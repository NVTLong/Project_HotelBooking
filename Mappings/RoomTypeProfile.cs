using AutoMapper;
using Project_HotelBooking.Application.DTOs.RoomType;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeDto>();
            CreateMap<RoomTypeCreateDto, RoomType>();
            CreateMap<RoomTypeUpdateDto, RoomType>().ReverseMap();
        }
    }
}
