using AutoMapper;
using Project_HotelBooking.Application.DTOs.Promotion;
using Project_HotelBooking.Models;
using Project_HotelBooking.ViewModels.Promotion;

namespace Project_HotelBooking.Mappings
{
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Promotion, PromotionUpdateDto>().ReverseMap();
            CreateMap<PromotionDto, PromotionVM>().ReverseMap();
            CreateMap<PromotionUpdateDto, PromotionVM>().ReverseMap();
        }
    }
}
