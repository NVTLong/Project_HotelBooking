using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Identity;

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

            return services;
        }
    }
}
