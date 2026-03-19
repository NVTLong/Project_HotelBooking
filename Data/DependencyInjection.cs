using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Application.Services;
using Project_HotelBooking.Identity;
using Project_HotelBooking.Repository;

namespace Project_HotelBooking.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<AppUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Đăng ký Repository
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomAmenityRepository, RoomAmenityRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
            services.AddScoped<IHotelServiceRepository, HotelServiceRepository>();
            services.AddScoped<IBookingHotelServiceRepository, BookingHotelServiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            // Đăng ký Service
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAmenityService, AmenityService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingDetailService, BookingDetailService>();
            services.AddScoped<IHotelServiceService, HotelServiceService>();
            services.AddScoped<IBookingHotelServiceService, BookingHotelServiceService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
