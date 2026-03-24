using AutoMapper;
using Project_HotelBooking.Application.DTOs.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class HotelServiceProfile : Profile
    {
        public HotelServiceProfile() 
        {
            CreateMap<HotelService, HotelServiceDto>();
            CreateMap<HotelServiceCreateDto, HotelService>();
            CreateMap<HotelServiceUpdateDto, HotelService>();
        }
    }
}
