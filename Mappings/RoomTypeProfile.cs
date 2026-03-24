using AutoMapper;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Application.DTOs.RoomType;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeDto>();
            CreateMap<RoomTypeImage, RoomTypeImageDto>();
            CreateMap<RoomTypeCreateDto, RoomType>();
            CreateMap<RoomTypeUpdateDto, RoomType>().ReverseMap();
            CreateMap<RoomImage, RoomImageDto>();
        }
    }
}
