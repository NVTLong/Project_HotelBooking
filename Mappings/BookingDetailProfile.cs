using AutoMapper;
using Project_HotelBooking.Application.DTOs.BookingDetail;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class BookingDetailProfile : Profile
    {
        public BookingDetailProfile()
        {
            CreateMap<BookingDetail, BookingDetailDto>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.HotelServices, opt => opt.MapFrom(src => src.BookingHotelServices));

            CreateMap<BookingDetailCreateDto, BookingDetail>();

            CreateMap<BookingDetailUpdateDto, BookingDetail>();
        }
    }
}
