using AutoMapper;
using Project_HotelBooking.Application.DTOs.BookingHotelService;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class BookingHotelServiceProfile : Profile
    {
        public BookingHotelServiceProfile()
        {
            CreateMap<BookingHotelServiceCreateDto, BookingHotelService>();

            CreateMap<BookingHotelServiceItemDto, BookingHotelService>();

            CreateMap<BookingHotelService, BookingHotelServiceDetailDto>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.HotelService.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
