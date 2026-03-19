using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_HotelBooking.Identity;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>

    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region DbSet

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeImage> RoomTypeImages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<BookingHotelService> BookingHotelServices { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Floor - Room
            builder.Entity<Room>()
                .HasOne(r => r.Floor)
                .WithMany(f => f.Rooms)
                .HasForeignKey(r => r.FloorId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoomType - Room
            builder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoomType - RoomTypeImage
            builder.Entity<RoomTypeImage>()
                .HasOne(rti => rti.RoomType)
                .WithMany(rt => rt.RoomTypeImages)
                .HasForeignKey(rti => rti.RoomTypeId);

            // Room - RoomImage
            builder.Entity<RoomImage>()
                .HasOne(ri => ri.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(ri => ri.RoomId);

            // RoomAmenity (many-many)
            builder.Entity<RoomAmenity>()
                .HasKey(ra => new { ra.RoomId, ra.AmenityId });

            builder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(ra => ra.RoomId);

            builder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Amenity)
                .WithMany(a => a.RoomAmenities)
                .HasForeignKey(ra => ra.AmenityId);

            // Booking - Customer
            builder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            // BookingDetail
            builder.Entity<BookingDetail>()
                .HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(bd => bd.BookingId);

            builder.Entity<BookingDetail>()
                .HasOne(bd => bd.Room)
                .WithMany(r => r.BookingDetails)
                .HasForeignKey(bd => bd.RoomId);

            // BookingService
            builder.Entity<BookingHotelService>()
                .HasOne(bs => bs.BookingDetail)
                .WithMany(b => b.BookingHotelServices)
                .HasForeignKey(bs => bs.BookingDetailId);

            builder.Entity<BookingHotelService>()
                .HasOne(bs => bs.HotelService)
                .WithMany(s => s.BookingServices)
                .HasForeignKey(bs => bs.ServiceId);

            // Payment
            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId);

            // ==============================
            // Tránh trùng lặp dữ liệu
            // ==============================

            // 1. Room: Số phòng không được trùng trong cùng tầng
            builder.Entity<Room>()
                .HasIndex(r => new { r.FloorId, r.RoomNumber })
                .IsUnique();


            // 2. Floor: Số tầng không được trùng
            builder.Entity<Floor>()
                .HasIndex(f => f.FloorNumber)
                .IsUnique();


            // 3. RoomType: Tên loại phòng không được trùng
            builder.Entity<RoomType>()
                .HasIndex(rt => rt.Name)
                .IsUnique();


            // 4. Service: Tên dịch vụ không được trùng
            builder.Entity<HotelService>()
                .HasIndex(s => s.Name)
                .IsUnique();


            // 5. Amenity: Tên tiện nghi không được trùng
            builder.Entity<Amenity>()
                .HasIndex(a => a.Name)
                .IsUnique();


            // 6. Promotion / Voucher: Mã khuyến mãi không được trùng
            builder.Entity<Promotion>()
                .HasIndex(p => p.Code)
                .IsUnique();


            // 7. User / Customer: Email không được trùng
            builder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

        }

        public static async Task SeedAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            // ============================
            // 1️ Seed Roles
            // ============================

            string[] roles = { "Admin", "Receptionist", "Accountant", "Customer", "Staff" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // ============================
            // 2️ Seed Admin User
            // ============================

            var adminEmail = "admin@hotel.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                // Nếu admin đã tồn tại nhưng chưa có role
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
