using AutoMapper;
using Project_HotelBooking.Application.DTOs.Customer;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerCreateDto, Customer>();

            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        }
    }
}
