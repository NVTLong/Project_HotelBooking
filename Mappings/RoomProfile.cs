using AutoMapper;
using Project_HotelBooking.Application.DTOs.Room;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomUpdateDto, Room>();
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.FloorName))
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.RoomType.BasePrice))
                .ForMember(dest => dest.AmenityIds, opt => opt.MapFrom(src => src.RoomAmenities.Select(x => x.AmenityId)))
                .ForMember(dest => dest.RoomImages, opt => opt.MapFrom(src => src.RoomImages));

        }
    }
}
