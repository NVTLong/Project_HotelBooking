using AutoMapper;
using Project_HotelBooking.Application.DTOs.Amenity;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile() 
        { 
            CreateMap<Amenity, AmenityDto>();
            CreateMap<AmenityDto, Amenity>();
            CreateMap<AmenityUpdateDto, Amenity>().ReverseMap();
        }
    }
}
