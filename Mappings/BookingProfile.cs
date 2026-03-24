using AutoMapper;
using Project_HotelBooking.Application.DTOs.Booking;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile() {
            CreateMap<Booking, BookingDto>()
                    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer!.FullName));

            CreateMap<BookingCreateDto, Booking>();

            CreateMap<BookingUpdateDto, Booking>();

        }
    }
}
